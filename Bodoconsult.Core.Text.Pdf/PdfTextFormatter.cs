// Copyright (c) Bodoconsult EDV-Dienstleistungen GmbH. All rights reserved.

using System;
using System.Data;
using System.IO;
using Bodoconsult.Core.Pdf.PdfSharp;
using Bodoconsult.Core.Pdf.Stylesets;
using Bodoconsult.Core.Text.Enums;
using Bodoconsult.Core.Text.Interfaces;
using Bodoconsult.Core.Text.Model;
using MigraDoc.DocumentObjectModel;

namespace Bodoconsult.Core.Text.Pdf
{
    public class PdfTextFormatter : ITextFormatter
    {

        public int Schritt { get; set; }

        public IStyleSet StyleSet { get; set; }


        public Color BackColor { get; set; }
        public Color TableBorderColor { get; set; }
        public Color HeadLineColor { get; set; }
        public Color DetailsColor { get; set; }

        public string LogoImagePath { get; set; }

        public string Subject { get; set; }

        public string Author { get; set; }

        public string GetFormattedText()
        {
            var dl = new DataTable();
            dl.Columns.Add("Left");
            dl.Columns.Add("Right");


            Pdf = new PdfCreator(StyleSet)
            {
                Increment = Schritt,
                BackColor = BackColor,
                TableBorderColor = TableBorderColor
            };

            Pdf.SetDocInfo(Title, Subject, Author);
            Pdf.SetHeader(Author, "Header", LogoImagePath);
            Pdf.SetFooter($"{DateTime.Now:dd.MM.yyyy}\t<<page>>");
            // Pdf.BackgroundImagePath = BackgroundImagePath;
            Pdf.CreateTocSection("Inhaltsverzeichnis");
            Pdf.CreateContentSection();



            for (var i = 0; i < StructuredText.TextItems.Count; i++)
            {

                var ti = StructuredText.TextItems[i];
                var content = ti.Content.Replace("??br??", "\r\n");

                switch (ti.LogicalType)
                {
                    case TextItemType.H1:
                        Pdf.AddParagraph(content, "Heading1");
                        break;
                    case TextItemType.H2:
                        Pdf.AddParagraph(content, "Heading2");
                        break;
                    case TextItemType.H3:
                        Pdf.AddParagraph(content, "Heading3");
                        break;
                    case TextItemType.H4:
                        Pdf.AddParagraph(content, "Heading4");
                        break;
                    case TextItemType.P:
                        Pdf.AddParagraph(content, "Normal");
                        break;
                    case TextItemType.ListItem:
                        Pdf.AddParagraph(content, "Bullet1");
                        break;
                    case TextItemType.DefinitionListLine:

                        var isfirst = false;
                        var islast = false;

                        if (i == 0)
                        {
                            isfirst = true;
                        }
                        else if (i == StructuredText.TextItems.Count - 1)
                        {
                            islast = true;
                        }
                        else
                        {
                            var itemBefore = StructuredText.TextItems[i - 1];
                            if (itemBefore.LogicalType != TextItemType.DefinitionListLine) isfirst = true;

                            var itemAfter = StructuredText.TextItems[i + 1];
                            if (itemAfter.LogicalType != TextItemType.DefinitionListLine) islast = true;
                        }

                        if (isfirst)
                        {
                            dl.Clear();

                        }

                        var dlitem = (DefinitionListTextItem)ti;

                        var row = dl.Rows.Add();
                        row["Left"] = dlitem.Content;
                        row["Right"] = dlitem.Content2;

                        if (islast)
                        {
                            Pdf.AddDefinitionList(dl, "Normal", "Normal");
                        }
                        break;
                    case TextItemType.Xml:
                    case TextItemType.Code:
                        Pdf.AddParagraph(content, "Code");
                        break;
                    case TextItemType.Table:
                        GetTable((TableTextItem)ti);
                        break;
                    default:

                        break;
                }


            }

            return null;
        }

        private void GetTable(TableTextItem ti)
        {
            var data = new DataTable();

            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream);
                writer.Write(ti.DataTableXml);
                writer.Flush();
                stream.Position = 0;
                data.ReadXml(stream);
            }

            Pdf.AddTable(data, ti.Content, "NoHeading1", "", "", Pdf.Width);

        }




        /// <summary>Structured text to format</summary>
        public IStructuredText StructuredText { get; set; }

        /// <summary>
        /// Template to place the structered text. Must contain placeholder {0} for the structured text
        /// </summary>
        public string Template { get; set; }

        /// <summary>Title for documentation</summary>
        public string Title { get; set; }

        /// <summary>
        /// Show the date the text was created. I.e.: "date created: 7.7.2018"
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// Save as file
        /// </summary>
        /// <param name="fileName">Full path for the PDF file</param>
        public void SaveAsFile(string fileName)
        {
            Pdf.RenderToPdf(fileName, false);
        }




        public PdfCreator Pdf;

        public PdfTextFormatter()
        {
            BackColor = new Color(211, 223, 240);
            TableBorderColor = new Color(217, 230, 245);
            HeadLineColor = Colors.Black;
            DetailsColor = Colors.Black;

            StyleSet = new DefaultStyleSet();

        }

        /// <summary>
        /// ctor with a stylesheet for customized formatting
        /// </summary>
        /// <param name="styleSet"></param>
        public PdfTextFormatter(IStyleSet styleSet)
        {
            BackColor = new Color(211, 223, 240);
            TableBorderColor = new Color(217, 230, 245);
            HeadLineColor = Colors.Black;
            DetailsColor = Colors.Black;

            StyleSet = styleSet;

        }


        public void Dispose()
        {
            try
            {
                Pdf.Dispose();
            }
            catch
            {
                // ignored
            }
        }


    }
}

