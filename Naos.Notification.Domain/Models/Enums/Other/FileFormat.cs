// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileFormat.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Notification.Domain
{
    using System.CodeDom.Compiler;

    /// <summary>
    /// Specifies the format of the file.
    /// </summary>
    /// <remarks>
    /// This is copy/pasted from Naos.Slack.Domain.FileType.  Keep them in-sync.
    /// </remarks>
    [GeneratedCode("Ignore all CA warnings", "Ignore all CA warnings")]
    public enum FileFormat
    {
        /// <summary>
        /// Unspecified (default).
        /// </summary>
        Unspecified,

        /// <summary>
        /// Plain Text.
        /// </summary>
        Text,

        /// <summary>
        /// Illustrator File.
        /// </summary>
        Ai,

        /// <summary>
        /// APK.
        /// </summary>
        Apk,

        /// <summary>
        /// AppleScript.
        /// </summary>
        AppleScript,

        /// <summary>
        /// Binary.
        /// </summary>
        Binary,

        /// <summary>
        /// Bitmap.
        /// </summary>
        Bmp,

        /// <summary>
        /// BoxNote.
        /// </summary>
        BoxNote,

        /// <summary>
        /// C.
        /// </summary>
        C,

        /// <summary>
        /// C#.
        /// </summary>
        CSharp,

        /// <summary>
        /// C++.
        /// </summary>
        Cpp,

        /// <summary>
        /// CSS.
        /// </summary>
        Css,

        /// <summary>
        /// CSV.
        /// </summary>
        Csv,

        /// <summary>
        /// Clojure.
        /// </summary>
        Clojure,

        /// <summary>
        /// CoffeeScript.
        /// </summary>
        CoffeeScript,

        /// <summary>
        /// ColdFusion.
        /// </summary>
        Cfm,

        /// <summary>
        /// D.
        /// </summary>
        D,

        /// <summary>
        /// Dart.
        /// </summary>
        Dart,

        /// <summary>
        /// Diff.
        /// </summary>
        Diff,

        /// <summary>
        /// Word Document.
        /// </summary>
        Doc,

        /// <summary>
        /// Word document.
        /// </summary>
        Docx,

        /// <summary>
        /// Docker.
        /// </summary>
        DockerFile,

        /// <summary>
        /// Word template.
        /// </summary>
        Dotx,

        /// <summary>
        /// Email.
        /// </summary>
        Email,

        /// <summary>
        /// EPS.
        /// </summary>
        Eps,

        /// <summary>
        /// EPUB.
        /// </summary>
        Epub,

        /// <summary>
        /// Erlang.
        /// </summary>
        Erlang,

        /// <summary>
        /// Flash FLA.
        /// </summary>
        Fla,

        /// <summary>
        /// Flash video.
        /// </summary>
        Flv,

        /// <summary>
        /// F#.
        /// </summary>
        FSharp,

        /// <summary>
        /// Fortran.
        /// </summary>
        Fortran,

        /// <summary>
        /// GDocs Document.
        /// </summary>
        GDoc,

        /// <summary>
        /// GDocs Drawing.
        /// </summary>
        GDraw,

        /// <summary>
        /// GIF.
        /// </summary>
        Gif,

        /// <summary>
        /// Go.
        /// </summary>
        Go,

        /// <summary>
        /// GDocs Presentation.
        /// </summary>
        GPres,

        /// <summary>
        /// Groovy.
        /// </summary>
        Groovy,

        /// <summary>
        /// GDocs Spreadsheet.
        /// </summary>
        GSheet,

        /// <summary>
        /// GZip.
        /// </summary>
        GZip,

        /// <summary>
        /// HTML.
        /// </summary>
        Html,

        /// <summary>
        /// Handlebars.
        /// </summary>
        Handlebars,

        /// <summary>
        /// Haskell.
        /// </summary>
        Haskell,

        /// <summary>
        /// Haxe.
        /// </summary>
        Haxe,

        /// <summary>
        /// InDesign Document.
        /// </summary>
        Indd,

        /// <summary>
        /// Java.
        /// </summary>
        Java,

        /// <summary>
        /// JavaScript/JSON.
        /// </summary>
        JavaScript,

        /// <summary>
        /// JPEG.
        /// </summary>
        Jpg,

        /// <summary>
        /// Keynote Document.
        /// </summary>
        Keynote,

        /// <summary>
        /// Kotlin.
        /// </summary>
        Kotlin,

        /// <summary>
        /// LaTeX/sTeX.
        /// </summary>
        Latex,

        /// <summary>
        /// Lisp.
        /// </summary>
        Lisp,

        /// <summary>
        /// Lua.
        /// </summary>
        Lua,

        /// <summary>
        /// MPEG 4 audio.
        /// </summary>
        M4a,

        /// <summary>
        /// Markdown (raw).
        /// </summary>
        Markdown,

        /// <summary>
        /// MATLAB.
        /// </summary>
        Matlab,

        /// <summary>
        /// MHTML.
        /// </summary>
        MHtml,

        /// <summary>
        /// Matroska video.
        /// </summary>
        Mkv,

        /// <summary>
        /// QuickTime video.
        /// </summary>
        Mov,

        /// <summary>
        /// mp4.
        /// </summary>
        Mp3,

        /// <summary>
        /// MPEG 4 video.
        /// </summary>
        Mp4,

        /// <summary>
        /// MPEG video.
        /// </summary>
        Mpg,

        /// <summary>
        /// MUMPS.
        /// </summary>
        Mumps,

        /// <summary>
        /// Numbers Document.
        /// </summary>
        Numbers,

        /// <summary>
        /// NZB.
        /// </summary>
        Nzb,

        /// <summary>
        /// Objective-C.
        /// </summary>
        ObjC,

        /// <summary>
        /// OCaml.
        /// </summary>
        OCaml,

        /// <summary>
        /// OpenDocument Drawing.
        /// </summary>
        Odg,

        /// <summary>
        /// OpenDocument Image.
        /// </summary>
        Odi,

        /// <summary>
        /// OpenDocument Presentation.
        /// </summary>
        Odp,

        /// <summary>
        /// OpenDocument Spreadsheet.
        /// </summary>
        Ods,

        /// <summary>
        /// OpenDocument Text.
        /// </summary>
        Odt,

        /// <summary>
        /// Ogg Vorbis.
        /// </summary>
        Ogg,

        /// <summary>
        /// Ogg video.
        /// </summary>
        Ogv,

        /// <summary>
        /// Pages Document.
        /// </summary>
        Pages,

        /// <summary>
        /// Pascal.
        /// </summary>
        Pascal,

        /// <summary>
        /// PDF.
        /// </summary>
        Pdf,

        /// <summary>
        /// Perl.
        /// </summary>
        Perl,

        /// <summary>
        /// PHP.
        /// </summary>
        Php,

        /// <summary>
        /// Pig.
        /// </summary>
        Pig,

        /// <summary>
        /// PNG.
        /// </summary>
        Png,

        /// <summary>
        /// Slack Post.
        /// </summary>
        Post,

        /// <summary>
        /// PowerShell.
        /// </summary>
        PowerShell,

        /// <summary>
        /// PowerPoint presentation.
        /// </summary>
        Ppt,

        /// <summary>
        /// PowerPoint presentation.
        /// </summary>
        Pptx,

        /// <summary>
        /// Photoshop Document.
        /// </summary>
        Psd,

        /// <summary>
        /// Puppet.
        /// </summary>
        Puppet,

        /// <summary>
        /// Python.
        /// </summary>
        Python,

        /// <summary>
        /// Quartz Composer Composition.
        /// </summary>
        Qtz,

        /// <summary>
        /// R.
        /// </summary>
        R,

        /// <summary>
        /// Rich Text File.
        /// </summary>
        Rtf,

        /// <summary>
        /// Ruby.
        /// </summary>
        Ruby,

        /// <summary>
        /// Rust.
        /// </summary>
        Rust,

        /// <summary>
        /// SQL.
        /// </summary>
        Sql,

        /// <summary>
        /// Sass.
        /// </summary>
        Sass,

        /// <summary>
        /// Scala.
        /// </summary>
        Scala,

        /// <summary>
        /// Scheme.
        /// </summary>
        Scheme,

        /// <summary>
        /// Sketch File.
        /// </summary>
        Sketch,

        /// <summary>
        /// Shell.
        /// </summary>
        Shell,

        /// <summary>
        /// Smalltalk.
        /// </summary>
        Smalltalk,

        /// <summary>
        /// SVG.
        /// </summary>
        Svg,

        /// <summary>
        /// Flash SWF.
        /// </summary>
        Swf,

        /// <summary>
        /// Swift.
        /// </summary>
        Swift,

        /// <summary>
        /// Tarball.
        /// </summary>
        Tar,

        /// <summary>
        /// TIFF.
        /// </summary>
        Tiff,

        /// <summary>
        /// TSV.
        /// </summary>
        Tsv,

        /// <summary>
        /// VB.NET.
        /// </summary>
        Vb,

        /// <summary>
        /// VBScript.
        /// </summary>
        VbScript,

        /// <summary>
        /// vCard.
        /// </summary>
        VCard,

        /// <summary>
        /// Velocity.
        /// </summary>
        Velocity,

        /// <summary>
        /// Verilog.
        /// </summary>
        Verilog,

        /// <summary>
        /// Waveform audio.
        /// </summary>
        Wav,

        /// <summary>
        /// WebM.
        /// </summary>
        WebM,

        /// <summary>
        /// Windows Media Video.
        /// </summary>
        Wmv,

        /// <summary>
        /// Excel spreadsheet.
        /// </summary>
        Xls,

        /// <summary>
        /// Excel spreadsheet.
        /// </summary>
        Xlsx,

        /// <summary>
        /// Excel Spreadsheet (Binary, Macro Enabled).
        /// </summary>
        Xlsb,

        /// <summary>
        /// Excel Spreadsheet (Macro Enabled).
        /// </summary>
        Xlsm,

        /// <summary>
        /// Excel template.
        /// </summary>
        Xltx,

        /// <summary>
        /// XML.
        /// </summary>
        Xml,

        /// <summary>
        /// YAML.
        /// </summary>
        Yaml,

        /// <summary>
        /// Zip.
        /// </summary>
        Zip,
    }
}