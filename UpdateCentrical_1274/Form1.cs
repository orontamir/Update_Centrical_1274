using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpdateCentrical_1274.DataModel;
using UpdateCentrical_1274.Email;
using UpdateCentrical_1274.RestFull;

namespace UpdateCentrical_1274
{
    public partial class Form1 : Form
    {
        ManualResetEvent m_stopThreadsEvent = new ManualResetEvent(false);

        private readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Form1));
        private readonly int _timeToWait = int.Parse(ConfigurationManager.AppSettings["timeToWait"]);
        private readonly int _textBoxLines = int.Parse(ConfigurationManager.AppSettings["textBoxLines"]); 
        public Form1()
        {
            InitializeComponent();
        }
        
        private void startBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = false;
            stopbtn.Enabled = true;
            m_stopThreadsEvent = new ManualResetEvent(false);
            Thread t1 = new Thread(new ThreadStart(Run));
            t1.Start();          
        }

        [Obsolete("Message")]
        private void Run()
        {
            while (true)
            {
                try
                {
                    AppendText("Start Update seler in centrical");
                    IEnumerable<Seler> selers = DataBase.DBParser.Instance().GetAllSelers();
                    List<Seler> updateOk = new List<Seler>();
                    List<Seler> errorUpdate = new List<Seler>();
                    foreach (Seler seler in selers)
                    {
                        var result = RestApi.Instance().POST(seler.ToString());
                        if (result.Contains("Error message") || result.Contains("false"))
                        {
                            seler.ErrorMessage = $"Seler SSN {seler.SelerSsn} did not succeeded update in centrical";
                            AppendText(seler.ErrorMessage);
                            errorUpdate.Add(seler);
                        }
                        else
                        {
                            AppendText($"Successfully update Seler SSN {seler.SelerSsn}  in centrical");
                            var isUpdate = DataBase.DBParser.Instance().UpdateSelerStatus(seler.IdNum);
                            if (isUpdate)
                            {
                                AppendText($"Successfully update Seler SSN status {seler.SelerSsn}  in data base");
                                updateOk.Add(seler);
                            }
                            else
                            {
                                seler.ErrorMessage = $"Seler SSN {seler.SelerSsn} did not succeeded update seler status Data base";
                                AppendText(seler.ErrorMessage);
                                errorUpdate.Add(seler);
                            }
                        }
                    }
                    if (updateOk.Count > 0 || errorUpdate.Count > 0)
                    {
                        AppendText($"{updateOk.Count} selers where update and {errorUpdate.Count} selers getting error");
                        //send mail
                        string title = $"ממשק גיוס לקוחות אפליקציה -  מוכרנים סנטריקל";
                        string body = $"<div  style='direction:rtl' > שלום  <br /> מספר המוכרנים שעודכנו בסנטריקל {updateOk.Count} <br /> מספר המוכרנים שלא עודכנו בסנטריקל {errorUpdate.Count} </div>";
                        if (errorUpdate.Count > 0)
                        {
                            string errorSeler = "<div  style='direction:rtl' ><br />מוכרנים שלא עודכנו בסנטריקל<br /></div>";
                            foreach (Seler seler in errorUpdate)
                            {
                                errorSeler += $"<div  style='direction:rtl' ><br />{seler.ToString()}<br /></div>";
                            }
                            body += errorSeler;
                        }
                        SendMail.Instance().Email(body, title);
                    }
                    AppendText("Finish Update seler in centrical");
                }
                catch (Exception ex)
                {
                    AppendText($"Error when try to update selers, error message: {ex.Message}");
                }
                if (m_stopThreadsEvent.WaitOne(_timeToWait))
                {
                    break;
                }
            }
        }

        delegate void AppendTextDelegate(string text);

        private void AppendText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new AppendTextDelegate(this.AppendText), new object[] { text });
            }
            else
            {
                textBox1.Text += $"{DateTime.Now.ToString()}: {text}\r\n";
                log.Debug(text);
                if (textBox1.Lines.Count() > _textBoxLines)
                {
                    textBox1.Lines = textBox1.Lines.Skip(1).ToArray();
                }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_stopThreadsEvent.Set();
            Thread.Sleep(2000);
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            stopbtn.Enabled = false;
            startBtn.Enabled = true;
            m_stopThreadsEvent.Set();
            AppendText("Stop Update seler in centrical");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void OpenLogBtn_Click(object sender, EventArgs e)
        {
            string FileName = "";
            try
            {
                FileName =@"c:\Log\"+ DateTime.Now.ToString("yyyy-MM-dd")+ "_UpdateCentrical.txt";//ClsApi.FldLog + "\\Log" + DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = "notepad++.exe";
                proc.StartInfo.Arguments = FileName;
                proc.Start();
            }
            catch (Exception ex)
            {

                log.Error($"Rrror when try to open log {FileName}, error message: {ex.Message}");

            }

        }

        private void OpenLogFolderBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = "";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = @"c:\Log\";
                proc.StartInfo.Arguments = FileName;
                proc.Start();
            }
            catch (Exception ex)
            {
                log.Error($"Rrror when try to open log folder c:\\Log\\, error message: {ex.Message}"); ;
            }

        }
    }
}
