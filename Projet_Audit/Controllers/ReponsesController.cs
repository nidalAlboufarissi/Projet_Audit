using Projet_Audit.Models;
using Syncfusion.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Projet_Audit.Controllers
{
    [Authorize]
    public class ReponsesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        //Store answers
         [HttpPost]
        public ActionResult Create(FormCollection  reponses)
        {
            DateTime date = DateTime.Now;
            UserEnregistrement enregistrement = new UserEnregistrement();
            enregistrement.Date = date;
            ApplicationUser user = db.Users.Where(u=> u.UserName==User.Identity.Name.ToString()).FirstOrDefault();
            enregistrement.UserName = user.UserName;
            enregistrement.ApplicationUser = user;
            List<UserResponse> list = new List<UserResponse>();
            foreach (var item in db.Questions.ToList())
            {
                string reponse = reponses["reponse"+item.Id_question];
                UserResponse userResponse = new UserResponse();
              
                //Check with the answer of the database 
                if (reponse != null)
                {
                    if (reponse.Equals("oui"))
                    {
                        if (item.Reponse == true)
                        {
                            userResponse.Reponse = true;
                            userResponse.score = item.Coefficient;
                        }
                        else if (item.Reponse == false)
                        {
                            userResponse.score = 0;
                            userResponse.Reponse = true;
                        }

                    }
                    else if (reponse.Equals("non"))
                    {
                        if (item.Reponse == false)
                        {
                            userResponse.Reponse = false;
                            userResponse.score = item.Coefficient;
                        }
                        else if (item.Reponse == true)
                        {
                            userResponse.Reponse = false;
                            userResponse.score = 0;
                        }
                    }
                }
                else
                {
                    userResponse.score = -1;

                }
                
                userResponse.Question = item;
                list.Add(userResponse);
            }
            enregistrement.UserResponses = list;
            db.UserEnregistrements.Add(enregistrement);
            List<UserRisque> listUR = new List<UserRisque>();
            foreach(var r in db.Risques.ToList())
            {
                UserRisque userRisque = new UserRisque();
                int nbrQst = r.Questions.Count;
                int nbrQst2 = 0;
                int nbrOui = 0;
                int nbrNon = 0;
                int nbrNull = 0;
            
                foreach (var u in list)
                {
                    if(u.Question.Id_risque==r.Id_risque)
                    {
                        if (u.score == -1)
                        {
                            nbrNull++;
                        }
                       else if (u.score == 0)
                        {
                            nbrQst2++;
                            nbrNon++;
                        }
                        else if (u.score > 0)
                        {
                            nbrQst2++;
                            nbrOui++;
                        }
                    }
                }
                userRisque.Id_risque = r.Id_risque;
                userRisque.Risque = r;
                if (nbrQst == nbrQst2)
                    userRisque.Status = "Traité";
                else userRisque.Status = "Non Traité";
                userRisque.NombreQst = nbrQst;
                userRisque.QstTraite = nbrQst2;
                userRisque.NombreOui = nbrOui;
                userRisque.NombreNull = nbrNull;
                userRisque.NombreNon = nbrNon;
                listUR.Add(userRisque);
            }
            enregistrement.UserRisques = listUR;
            db.SaveChanges();
            return RedirectToAction("Result");
          
        }

        public ActionResult Result()
        {
            ViewBag.risques = db.Risques.ToList();
            List<UserEnregistrement> list = db.UserEnregistrements.Where(user => user.UserName == User.Identity.Name.ToString()).OrderByDescending(m=>m.Date).ToList();
            return View(list);
        }
        public ActionResult Export(int? id)
        {
            List<UserRisque> ur = db.UserEnregistrements.Find(id).UserRisques.ToList();
            //Opens a PowerPoint presentation
            IPresentation sourcePresentation = Presentation.Open(Server.MapPath("~/Files/Rapport.pptx"));
            int cmp = ur.Count;
            //Clones the Presentation
            IPresentation clonedPresentation = sourcePresentation.Clone();

            //Gets the first slide from the cloned PowerPoint presentation
            ISlide firstSlide = clonedPresentation.Slides[1];

            //Adds a textbox in a slide by specifying its position and size
            IShape Title = firstSlide.AddTextBox(350, 20, 300, 80);

            //Adds a paragraph in the body of the textShape
            IParagraph paragraph = Title.TextBody.AddParagraph();

            //Adds a textPart in the paragraph
            ITextPart text = paragraph.AddTextPart("Audit de sécurité des SI");
            text.Font.FontSize = 44;
            text.UnderlineColor.SystemColor = Color.White;


            //Add a table to the slide
            ITable table = firstSlide.Shapes.AddTable(cmp+1, 5, 80, 120, 800, (cmp+1)*30);
            table.BuiltInStyle = BuiltInTableStyle.LightStyle1;
            table.HasBandedRows = false;
            table.HasHeaderRow = true;
            table.HasBandedColumns = true;
            table.HasFirstColumn = true;
            table.HasLastColumn = false;
            table.HasTotalRow = false;
            //Initialize index values to add text to table cells
            int rowIndex = 0, i;

            //Iterate row-wise cells and add text to it
            foreach (IRow rows in table.Rows)
            {
                i = 0;
                if (rowIndex == 0)
                {
                    foreach (ICell cell in rows.Cells)
                    {
                        if (i == 0)
                            cell.TextBody.AddParagraph("Type");
                       else if (i == 1)
                            cell.TextBody.AddParagraph("Status de Traitement");
                       else if (i == 2)
                            cell.TextBody.AddParagraph("Nombre de questions");
                       else if (i == 3)
                            cell.TextBody.AddParagraph("Questions traitées");
                       else if (i == 4)
                            cell.TextBody.AddParagraph("Questions à traiter");

                        i++;

                    }
                }
                else
                {
                    foreach (ICell cell in rows.Cells)
                    {
                        if (i == 0)
                            cell.TextBody.AddParagraph(ur[rowIndex-1].Risque.Type);
                        else if (i == 1)
                        {
                            if(ur[rowIndex - 1].Status.Equals("Traité"))
                                  cell.Fill.SolidFill.Color.SystemColor = Color.Green;
                            else
                                cell.Fill.SolidFill.Color.SystemColor = Color.Red;
                            cell.TextBody.AddParagraph(ur[rowIndex - 1].Status);
                        }
                        else if (i == 2)
                            cell.TextBody.AddParagraph(ur[rowIndex - 1].NombreQst.ToString());
                        else if (i == 3)
                            cell.TextBody.AddParagraph(ur[rowIndex - 1].QstTraite.ToString());
                        else if (i == 4)
                            cell.TextBody.AddParagraph((ur[rowIndex - 1].NombreQst - ur[rowIndex - 1].QstTraite).ToString());

                        i++;

                    }
                }

                rowIndex++;

            }
            //Gets the first slide from the cloned PowerPoint presentation
             firstSlide = clonedPresentation.Slides[2];

            //Adds a textbox in a slide by specifying its position and size
             Title = firstSlide.AddTextBox(350, 20, 300, 80);

            //Adds a paragraph in the body of the textShape
             paragraph = Title.TextBody.AddParagraph();

            //Adds a textPart in the paragraph
             text = paragraph.AddTextPart("Audit de sécurité des SI");
            text.Font.FontSize = 44;
            text.UnderlineColor.SystemColor = Color.White;

             Title = firstSlide.AddTextBox(80, 60, 800, 80);

            //Adds a paragraph in the body of the textShape
            paragraph = Title.TextBody.AddParagraph();

            //Adds a textPart in the paragraph
            text = paragraph.AddTextPart("Nb de risque traités : "+cmp);
            text.Font.FontSize = 18;
            text.UnderlineColor.SystemColor = Color.White;



            //Add a table to the slide
            table = firstSlide.Shapes.AddTable(cmp + 1, 5, 80, 120, 800, (cmp+1)*30);
            table.BuiltInStyle = BuiltInTableStyle.LightStyle1;
            table.HasBandedRows = false;
            table.HasHeaderRow = true;
            table.HasBandedColumns = true;
            table.HasFirstColumn = true;
            table.HasLastColumn = false;
            table.HasTotalRow = false;
            //Initialize index values to add text to table cells
             rowIndex = 0;

            //Iterate row-wise cells and add text to it
            foreach (IRow rows in table.Rows)
            {
                i = 0;
                if (rowIndex == 0)
                {
                    foreach (ICell cell in rows.Cells)
                    {
                        if (i == 0)
                            cell.TextBody.AddParagraph("Type");
                        else if (i == 1)
                            cell.TextBody.AddParagraph("Oui");
                        else if (i == 2)
                            cell.TextBody.AddParagraph("Non");
                        else if (i == 3)
                            cell.TextBody.AddParagraph("NA");
                        else if (i == 4)
                            cell.TextBody.AddParagraph("Score");

                        i++;

                    }
                }
                else
                {
                    foreach (ICell cell in rows.Cells)
                    {
                        if (i == 0)
                            cell.TextBody.AddParagraph(ur[rowIndex - 1].Risque.Type);
                        else if (i == 1)
                        {
                       
                            cell.TextBody.AddParagraph(ur[rowIndex - 1].NombreOui.ToString());
                        }
                        else if (i == 2)
                            cell.TextBody.AddParagraph(ur[rowIndex - 1].NombreNon.ToString());
                        else if (i == 3)
                            cell.TextBody.AddParagraph(ur[rowIndex - 1].NombreNull.ToString());
                        else if (i == 4)
                            cell.TextBody.AddParagraph(ur[rowIndex - 1].score.ToString());

                        i++;

                    }
                }

                rowIndex++;

            }

            return new PresentationResult(clonedPresentation, "Output.pptx", HttpContext.ApplicationInstance.Response);
        }
       

    }
    public class PresentationResult : ActionResult

    {

        //private members

        private IPresentation m_source;

        private string m_filename;

        private HttpResponse m_response;

        //Get/Set the filename of Presentation

        public string FileName

        {

            get

            {

                return m_filename;

            }

            set

            {

                m_filename = value;

            }

        }

        //Get the Presentation

        public IPresentation Source

        {

            get

            {

                return m_source as IPresentation;

            }

        }

        //Get the HTTP response

        public HttpResponse Response

        {

            get

            {

                return m_response;

            }

        }

        public PresentationResult(IPresentation source, string fileName, HttpResponse response)

        {

            //Assign values using the constructor

            this.FileName = fileName;

            this.m_source = source;

            m_response = response;

        }

        public override void ExecuteResult(ControllerContext context)

        {

            //Throw exception if context is null

            if (context == null)

                throw new ArgumentNullException("Context");

            //Save the Presentation to the client browser

            this.m_source.Save(FileName, FormatType.Pptx, Response);

        }

    }



}
