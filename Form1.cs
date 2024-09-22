namespace CsToDo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string[] str_lst;

            var sr = new StreamReader("data.txt"); //data.txt를 읽어오는 StreamReader 객체 sr 생성
            string content = sr.ReadToEnd(); //data.txt의 모든 내용 저장

            int count = 0;
            char target = '\n';
            foreach(char data in content)
            {
                if(data == target) { count++; } //content에서 읽어온 내용이 줄바꿈(\n)일 때, count++ (라인 갯수 확인)
            }

            str_lst = content.Split('\n'); //content를 \n 기준으로 나눠 리스트 형태로 str_lst에 저장

            
            for (int i = 0; i < count; i++)
            {
                string[] str_lst2 = str_lst[i].Split('\t');
                string[] strArray = new string[] { str_lst2[0], str_lst2[1], str_lst2[2], str_lst2[3] };
                //ListView에는 [상태, 할 일, 시작시점, 종료시점] 을 반드시 표기할 예정 -> index가 0 ~ 3 고정
                ListViewItem lvitem = new ListViewItem(strArray); 
                listView1.Items.Add(lvitem);
            }
            
            sr.Close(); //StreamReader 객체 종료
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var sw = new StreamWriter("data.txt", false);
            //data.txt에 데이터를 작성하기 위한 sw 객체 생성
            //false -> 만약, data.txt파일이 이미 존재한다면, 덮어쓰기 진행

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string data = listView1.Items[i].SubItems[0].Text + "\t" +
                    listView1.Items[i].SubItems[1].Text + "\t" +
                    listView1.Items[i].SubItems[2].Text + "\t" +
                    listView1.Items[i].SubItems[3].Text + "\n";
                //Itmes[i]는 i번째 행(row)을 뜻하며, SubItems[j]는 j번째 열(col)을 뜻함.

                sw.Write(data);
            }
            sw.Flush();
            sw.Close();
            this.Text = "data.txt";
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[0].Text == "완료") checkBox1.Checked = true;
            else { checkBox1.Checked=false; }
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[2].Text.Trim(new char[] { '“', '”' });
            dateTimePicker2.Text = listView1.SelectedItems[0].SubItems[3].Text.Trim(new char[] { '“', '”' });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") //TextBox칸에 내용이 없을 경우
            {
                MessageBox.Show("해야할 일을 모두 입력하세요.", "에러", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            
            string isSuccess; //상태(완료, 진행) 표시
            if (checkBox1.Checked == true) { isSuccess = "완료"; }
            else { isSuccess = "진행"; }

            string todo_str = textBox1.Text;
            string start_date = dateTimePicker1.Text;
            string end_date = dateTimePicker2.Text;

            string[] strArray = new string[]
            {
                isSuccess, todo_str, start_date, end_date
            };
            ListViewItem lvitem = new ListViewItem(strArray); //Listview에 내용(list형태) 추가
            listView1.Items.Add(lvitem);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) { listView1.SelectedItems[0].SubItems[0].Text = "완료"; }
            else { listView1.SelectedItems[0].SubItems[0].Text = "진행"; }
            listView1.SelectedItems[0].SubItems[1].Text = textBox1.Text;
            listView1.SelectedItems[0].SubItems[2].Text = dateTimePicker1.Text;
            listView1.SelectedItems[0].SubItems[3].Text = dateTimePicker2.Text;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            listView1.Items.Remove(listView1.SelectedItems[0]);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            var sw = new StreamWriter("data.txt",false);
            for(int i =0; i<listView1.Items.Count; i++)
            {
                string data = listView1.Items[i].SubItems[0].Text+"\t"+
                    listView1.Items[i].SubItems[1].Text+"\t"+
                    listView1.Items[i].SubItems[2].Text+"\t"+
                    listView1.Items[i].SubItems[3].Text+"\n";
                sw.Write(data);
            }
            sw.Flush();
            sw.Close();
            this.Text = "data.txt";
        }
    }
}
