# Naos.Notification

[![Build status](https://ci.appveyor.com/api/projects/status/637lo1k53q9mu4er?svg=true)](https://ci.appveyor.com/project/Naos-Project/naos-notification)

## Overview
An example of how to wire up notifications inside a system.

Subsystem Distributed Architecture Using Streams
==========================

Stream
--------
Below are the types of streams.  Streams are differentiated by their content, their producers (actors that write objects into the stream), and their consumers (actors that read objects from the stream).  Producers should have write-only access to the stream.  Consumers should have read-only access to the stream.  When handling (reacting to) Events or Operations in a Stream, the management of the state (e.g. an Event has not yet been handled) is an orthogonal concern from the perspective of read/write access to the Stream itself (i.e. handling an Event requires the ability to read that object from a Stream, but the system needs to write to manage the fact that the Event is being handled.  This is NOT considered write-access to the Stream, it is write-access to the handling management store adjacent to the Stream).

**[Subsystem] Client Operation Stream**
- *Purpose*: Allow external clients to queue work to be distributed by the system and serves as a single place to identify these requests.
- *Contents*: Contains the `IOperation` objects that the Subsystem will execute.  Often, these Operations are wrapped in an Event (e.g. `ExecuteOperationRequested<TId, TOperation>`), which allows the Subsystem to capture additional context such as an execution policy.
- *Producers*: Clients of the Subsystem.
- *Consumers*: The Subsystem handlers (typically Bots).

**[Subsystem] Internal Operation Stream**
- *Purpose*:  Allow the Subsystem to queue work and advance workflows of the Subsystem.
- *Contents*: Contains the `IOperation` objects that the Subsystem will execute.  Like the `Client Operation Stream`, these Operations are often wrapped in an Event.
- *Producers*: `Subsystem Event Stream Handler`
- *Consumers*: The Subsystem handlers (typically Bots).

**[Subsystem] Saga Stream**
- *Purpose*: Tracks one or more Operations that form a unit-of-work which drives a change in the state of an Aggregate.
- *Contents*: A saga; the information needed to track the outcome of executed Operations and instructions on the Event(s) to write based on those outcomes.
- *Producers*: Can be one of the following depending on how the work is produced:
    - `Client Operation Stream Handler`
    - `Internal Operation Stream Handler`
    - `Event Stream Handler`
- *Consumers*: The Subsystem handlers (typically Bots).

**[Subsystem] Event Stream**
- *Purpose*: Record changes to an Aggregate (when applied ordinally, reconstruct the current state of an Aggregate in the system).
- *Contents*: Contains the `IEvent`s which capture the changes to an Aggregate.
- *Producers*: Can be one of the following depending on the nature of the work inducing the change in state:
    - `Client Operation Stream Handler`
    - `Internal Operation Stream Handler`
    - `Saga Stream Handler`
- *Consumers*: 
    - The Subsystem handlers (typically Bots)
    - Clients of the Subsystem that require real-time access to the Aggregate.

**[Subsystem] View Stream**
- *Purpose*: Provide projections optimized for and intended to be queried raw (not de-serialized by the service that fetches the payload) and handed-off to the UI.
- *Contents*: Object whose serialized JSON is optimized for UI consumption.
- *Producers*: The Subsystem handlers (typically Bots).
- *Consumers*: API endpoints serving content to UI.

**[Subsystem] Snapshot Stream**
- *Purpose*: Get the Aggregate at some moment in time (some point in the Event Stream) to deburden the `Event Stream` and/or provide faster queries when real-time access to an Aggregate is not necessary.
- *Contents*: An object which is the Aggregate.
- *Producers*: The Subsystem handlers (typically Bots).
- *Consumers*: Clients

**[Subsystem Concern] Streams**
Some or all of the above streams can be used to address a specific concern of the Subsystem and in many ways treating it as its own discrete Subsystem.

Workflow
-----------
We'll use the example of building a Notification System to discuss the typical workflow of system-to-system communication and the management of distributed work.  A client of the  Notification System can request that a notification be sent to some user.  The Notification System will determine the user's preferred channel for communication and then send the notification to the appropriate channel (email, Slack, or both).  The client should be able to track the notification using a unique identifier.  Further, the Notification System should persist information about the completed work to enable auditing and querying after the fact (e.g. what emails have sent to test@example.com?).

- Client of the Notification System instantiates an Operation (e.g. `SendNotificationToEntityOp` in the `Notification.Domain` project containing the unique identifier of the entity to communicate with and details of the contents of the notification)
- Client executes the Operation via the associated Protocol (e.g. `SendNotificationToEntityProtocol` in the `Notification.Protocol.Client` project).
- The Client Protocol first gets a globally unique identifier from the `Notification Client Operation Stream`, and persists the Operation into that stream and returns the identifier to the client.  This identifies the notification and can be used by the client in the future to get the status of the notification using other Operations.
- Throughout the workflow below, the identifier will be used as the identifier for the various Events and Operations that are created so that we can query for them by that identifier.
- An Operation Handler picks up the Operation and executes it.  This involves:
    - Write an event that creates the Notification Aggregate to the `Notification Event Stream`.  This might be `CreatedEvent` or `CreatedEvent<SendNotificationToEntityOp>` or some other type that denotes the creation of an Aggregate.
    - Determine what channels the Entity in question prefers for communication.  Lets say that that's Email and Slack.
    - Deference email address and Slack workspace/channel.
    - Compose the Email and Slack messages to be sent in the form of Operations (e.g. `SendEmailOp` and `SendSlackMessageOp`), each containing all of the data required to compose and deliver the notification on the respective channels.
    - Enqueue the Operations in concern-specific Operations Streams (e.g. `Email Operation Stream` and `Slack Operation Stream`).
    - Enqueue a Saga into the `Notification Saga Stream` that points to the two previously enqueued Operations with an instruction that the Saga Handler should wait until both are complete or have failed (exception thrown - unlikely but could happen).  If complete, the handler should further inspect the resulting Events on the `Email Event Stream` and `Slack Event Stream` to ensure that the notifications were successfully sent or if there was a failure.  The branch logic should indicate what Event (e.g. `FailedToSendNotificationEvent` or `SucceededInSendingNotificationEvent`) write back to the `Notification Event Stream` to update the Aggregate based on the outcome of the executed Operations.
- The `Email Operation Handler` will handle the enqueued `SendEmailOp` and execute the operation (i.e. send an email).  When complete, it will write a success or failure event (e.g. `SucceededInSendingEmailEvent` or `FailedToSendEmailEvent`) to the concern-specific `Event Stream` (e.g. `Email Event Stream`).  In parallel, there will be a Slack handler following a similar workflow.
- When the Saga is completed, as noted above the Aggregate will be updated via an event put into the `Notification Event Stream`
- Reacting to a failure Event in the `Notification Event Stream`, a handler could enqueue an Operation to notify an internal team about the issue.  The Operation would be put into the `Notification Internal Operation Stream`
- An Operation Handler would pickup that Operation and execute it, which would entail:
    - Creating `SendEmailOp` addressed to the team and enqueueing it in the `Email Operation Stream`
    - Update the Aggregate by putting an Event in the `Notification Event Stream` (e.g. `FailedNotificationSentToInternalTeamEvent`).
> For example, the `Notification Event Stream`, for a single Notification, might have events like `RequestedNotificationToBeSentEvent` and `NotificationWasSentEvent`