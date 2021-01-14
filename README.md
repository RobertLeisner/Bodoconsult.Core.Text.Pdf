# What does the library

Bodoconsult.Core.Text.Pdf is an enhancement for Bodoconsult.Core.Text, a simple tool to generate structured text documents in a C# project. It enables export of a structured text to a PDF file.

We use Bodoconsult.Core.Text and Bodoconsult.Core.Text.Pdf for creating unit test logfiles intended for presentation to auditors. Another usage at Bodoconsult is creating an automated documentation for an app based on unit tests.

# How to use the library

The source code contain NUnit test classes, the following source code is extracted from. The samples below show the most helpful use cases for the library.

## Export as PDF (PdfTextFormatter class)

			var st = new StructuredText();

            st.AddHeader1("Überschrift 1");

            st.AddParagraph(TestHelper.Masstext1);

            var code = FileHelper.GetTextResource("code1.txt");

            st.AddCode(code);

            st.AddParagraph(TestHelper.Masstext1);

            st.AddDefinitionListLine("Left1", "Right1");
            st.AddDefinitionListLine("Left2", "Right2");
            st.AddDefinitionListLine("Left3", "Right3");
            st.AddDefinitionListLine("Left4", "Right4");

            st.AddParagraph(TestHelper.Masstext1);

            st.AddTable("Tabelle", TestHelper.GetDataTable());

            st.AddHeader1("Überschrift 2");

            st.AddDefinitionListLine("Left1", "Right1");
            st.AddDefinitionListLine("Left2", "Right2");
            st.AddDefinitionListLine("Left3", "Right3");
            st.AddDefinitionListLine("Left4", "Right4");

            var f = new PdfTextFormatter
            {
                Title = "Testreport",
                StructuredText = st,
                DateString = $"Date created: {DateTime.Now:G}",
                Author = "Testautor"

            };
            f.GetFormattedText();
            f.SaveAsFile(fileName);


## Export as HTML without template (HtmlTextFormatter class)

            var sr = new StructuredText();
            sr.AddHeader1("Überschrift 1 '& &&&' ");
            sr.AddParagraph(MassText, "CssTestFixture");
            sr.AddDefinitionListLine("Def1", "Value1");
            sr.AddDefinitionListLine("Definition 2", "Value1234");
            sr.AddDefinitionListLine("Defini 3", "Value234556666");
            sr.AddParagraph("");

            sr.AddParagraph(FormattedMasstext,
                "CssTestFixture");

            sr.AddCode(MassText, "CssTestFixture");

            sr.AddParagraph(MassText);

            sr.AddListItem("Bahnhof");
            sr.AddListItem("HauptBahnhof");
            sr.AddListItem("SüdBahnhof");
            sr.AddHeader2("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader2("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 1");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader2("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader2("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 1");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);

            var f = new HtmlTextFormatter { StructuredText = sr };
            f.GetFormattedText();
			
			f.SaveAsFile(fileName);

## Export as HTML with a individual template (HtmlTextFormatter class)

			var sr = new StructuredText();
            sr.AddHeader1("Überschrift 'Databanka'");
            sr.AddParagraph(MassText);
            sr.AddDefinitionListLine("Def1", "Value1");
            sr.AddDefinitionListLine("Definition 2", "Value1234");
            sr.AddDefinitionListLine("Defini 3", "Value234556666");
            sr.AddParagraph("");
            sr.AddParagraph(MassText);

            sr.AddListItem("Bahnhof");
            sr.AddListItem("HauptBahnhof");
            sr.AddListItem("SüdBahnhof");
            sr.AddHeader2("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader2("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 1");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader2("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader2("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 1");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);

            var f = new HtmlTextFormatter
            {
                StructuredText = sr,
                Template = "<<<Start>>>{0}<<<Ende>>>",
                AddTableOfContent = true
            };
            var result = f.GetFormattedText();
			
			f.SaveAsFile(fileName);

## Export as plain ASCII file (PlainTextFormatter class)

 var sr = new StructuredText();
            sr.AddHeader1("Überschrift 1");
            sr.AddParagraph(MassText);
            sr.AddDefinitionListLine("Def1", "Value1");
            sr.AddDefinitionListLine("Definition 2", "Value1234");
            sr.AddDefinitionListLine("Defini 3", "Value234556666");
            sr.AddParagraph("");
            sr.AddParagraph(MassText);

            sr.AddListItem("Bahnhof");
            sr.AddListItem("HauptBahnhof");
            sr.AddListItem("SüdBahnhof");

            sr.AddHeader1("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 1");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 2");
            sr.AddParagraph(MassText);
            sr.AddHeader1("Überschrift 1");
            sr.AddParagraph(MassText);
            sr.AddParagraph(MassText);

            var f = new PlainTextFormatter { StructuredText = sr };
            var result = f.GetFormattedText();
			
			f.SaveAsFile(fileName);


## Remarks

PdfSharp and Migradoc do not support stylesets by default- Same for PdfSharp.MigraDoc.netstandard 1.3.1. MigraDoc supports styles for styling PDF files.

Bodoconsult.Core.Pdf added styleset functionality. A styleset is defined as a list of MigraDoc styles. Therefore you can use all style settings supported by MigraDoc styles. 

With exception of the Normal style all styles defined in a styleset are injected as new styles to the MigraDoc PDF document. 

The Normal style exists in a MigraDoc PDF document by default. The Normal style from the styleset therefore has to cloned to the MigraDoc PDF document. 
It may happen that not all properties are cloned correctly.

# About us

Bodoconsult (<http://www.bodoconsult.de>) is a Munich based software company.

Robert Leisner is senior software developer at Bodoconsult. See his profile on <http://www.bodoconsult.de/Curriculum_vitae_Robert_Leisner.pdf>.

