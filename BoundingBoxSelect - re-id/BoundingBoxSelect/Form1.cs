using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BoundingBoxSelect
{
    public partial class Form1 : Form
    {

        #region Fields
        private string basepath = "";

        List<FrameArea.ImageInfo> img1, img2, img3, img4;
        int curPtr1, curPtr2, curPtr3, curPtr4;

        #endregion


        public Form1()
        {
            InitializeComponent();
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            Btn_all_backward.Enabled = false;
            Btn_all_forward.Enabled = false;

            int frameAreaWidth =  this.Width / 2;
            int frameAreaHeight = this.Height / 2 - 30;

            frameArea1.Width = frameAreaWidth;
            frameArea1.Height = frameAreaHeight;
            
            frameArea2.Width = frameAreaWidth;
            frameArea2.Height = frameAreaHeight;

            frameArea3.Width = frameAreaWidth;
            frameArea3.Height = frameAreaHeight;

            frameArea4.Width = frameAreaWidth;
            frameArea4.Height = frameAreaHeight;

            frameArea1.Location = new Point(0, 0);
            frameArea2.Location = new Point(frameAreaWidth, 0);
            frameArea3.Location = new Point(0, frameAreaHeight);
            frameArea4.Location = new Point(frameAreaWidth, frameAreaHeight);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (Btn_SetConfig.Enabled == false)
            {
                WriteAnnotationToFile(1, frameArea1.CurPtr, frameArea1.Image);
                WriteAnnotationToFile(2, frameArea2.CurPtr, frameArea2.Image);
                WriteAnnotationToFile(3, frameArea3.CurPtr, frameArea3.Image);
                WriteAnnotationToFile(4, frameArea4.CurPtr, frameArea4.Image);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderSelectDlg = new FolderBrowserDialog();
            folderSelectDlg.Description = "请选择文件夹路径";

            if (folderSelectDlg.ShowDialog() == DialogResult.OK)
            {
                string folderPath = folderSelectDlg.SelectedPath;
                TB_basepath.Text = folderPath;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(TB_basepath.Text))
            {
                MessageBox.Show("选择的路径不是一个文件夹路径");
                return;
            }
            Btn_SetConfig.Enabled = false;
            basepath = TB_basepath.Text;

            Btn_all_backward.Enabled = true;
            Btn_all_forward.Enabled = true;


            ReadAnnotationFromFile(1, ref curPtr1, ref img1);
            ReadAnnotationFromFile(2, ref curPtr2, ref img2);
            ReadAnnotationFromFile(3, ref curPtr3, ref img3);
            ReadAnnotationFromFile(4, ref curPtr4, ref img4);

            frameArea1.init(img1, curPtr1);
            frameArea2.init(img2, curPtr2);
            frameArea3.init(img3, curPtr3);
            frameArea4.init(img4, curPtr4);
            
            frameArea1.Invalidate();
            frameArea2.Invalidate();
            frameArea3.Invalidate();
            frameArea4.Invalidate();
        }


        private void ReadAnnotationFromFile(int idx, ref int curPtr, ref  List<FrameArea.ImageInfo> img)
        {
            img = new List<FrameArea.ImageInfo>();
            //diff image save in basepath/set_idx where idx is var
            DirectoryInfo TheFolder = new DirectoryInfo(Path.Combine(basepath, string.Format("set_{0:d}", idx)));
            foreach (FileInfo NextFile in TheFolder.GetFiles())
                img.Add(new FrameArea.ImageInfo(Path.Combine(new string[] { basepath, string.Format("set_{0:d}", idx), NextFile.Name })));

            // 读取之前所有的annotation
            try
            {
                FileStream fs = new FileStream(string.Format("annotation_{0:d}.txt", idx), FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(fs);
                string line = "";
                curPtr = -1;
                int id = 0;
                while (line != null)
                {
                    line = sr.ReadLine();
                    if (line == null || line.Equals(""))
                        continue;
                    int cnt = int.Parse(line);
                    ++curPtr;
                    for (int j = 0; j < cnt; ++j)
                    {
                        line = sr.ReadLine();
                        string[] num = line.Split(' ');
                        Rectangle anno = new Rectangle();
                        id = int.Parse(num[0]);
                        anno.X = int.Parse(num[1]);
                        anno.Y = int.Parse(num[2]);
                        anno.Width = int.Parse(num[3]);
                        anno.Height = int.Parse(num[4]);
                        img[curPtr].box.Add(anno);
                        img[curPtr].id_list.Add(id);
                    }
                }
                if (curPtr < 0)
                    curPtr = 0;
                sr.Close();
                fs.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WriteAnnotationToFile(int idx, int curPtr, List<FrameArea.ImageInfo> img)
        {
            //写入当前所有annotation
            try
            {
                //FileStream fs = new FileStream(string.Format("annotation_{0:d}.txt", idx), FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(string.Format("annotation_{0:d}.txt", idx), false);
                //sw.Write(img.Count);
                //curPtr max = img.box.count-1
                for (int i = 0; i <= curPtr; ++i)
                {
                    sw.WriteLine(img[i].box.Count);
                    for (int j = 0; j < img[i].box.Count; ++j)
                    {
                        sw.WriteLine(img[i].id_list[j] + " " + img[i].box[j].X + " " + img[i].box[j].Y + " " + img[i].box[j].Width + " " + img[i].box[j].Height);
                    }
                }
                sw.Close();
                //fs.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Btn_all_forward_Click(object sender, EventArgs e)
        {
            frameArea1.ToPrePicture();
            frameArea2.ToPrePicture();
            frameArea3.ToPrePicture();
            frameArea4.ToPrePicture();
        }

        private void Btn_all_backward_Click(object sender, EventArgs e)
        { 
            frameArea1.ToNextPicture();
            frameArea2.ToNextPicture();
            frameArea3.ToNextPicture();
            frameArea4.ToNextPicture();
        }
    }
}
