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

            var sr = new StreamReader("data.txt"); //data.txt�� �о���� StreamReader ��ü sr ����
            string content = sr.ReadToEnd(); //data.txt�� ��� ���� ����

            int count = 0;
            char target = '\n';
            foreach(char data in content)
            {
                if(data == target) { count++; } //content���� �о�� ������ �ٹٲ�(\n)�� ��, count++ (���� ���� Ȯ��)
            }

            str_lst = content.Split('\n'); //content�� \n �������� ���� ����Ʈ ���·� str_lst�� ����

            
            for (int i = 0; i < count; i++)
            {
                string[] str_lst2 = str_lst[i].Split('\t');
                string[] strArray = new string[] { str_lst2[0], str_lst2[1], str_lst2[2], str_lst2[3] };
                //ListView���� [����, �� ��, ���۽���, �������] �� �ݵ�� ǥ���� ���� -> index�� 0 ~ 3 ����
                ListViewItem lvitem = new ListViewItem(strArray); 
                listView1.Items.Add(lvitem);
            }
            
            sr.Close(); //StreamReader ��ü ����
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var sw = new StreamWriter("data.txt", false);
            //data.txt�� �����͸� �ۼ��ϱ� ���� sw ��ü ����
            //false -> ����, data.txt������ �̹� �����Ѵٸ�, ����� ����

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string data = listView1.Items[i].SubItems[0].Text + "\t" +
                    listView1.Items[i].SubItems[1].Text + "\t" +
                    listView1.Items[i].SubItems[2].Text + "\t" +
                    listView1.Items[i].SubItems[3].Text + "\n";
                //Itmes[i]�� i��° ��(row)�� ���ϸ�, SubItems[j]�� j��° ��(col)�� ����.

                sw.Write(data);
            }
            sw.Flush();
            sw.Close();
            this.Text = "data.txt";
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[0].Text == "�Ϸ�") checkBox1.Checked = true;
            else { checkBox1.Checked=false; }
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[2].Text.Trim(new char[] { '��', '��' });
            dateTimePicker2.Text = listView1.SelectedItems[0].SubItems[3].Text.Trim(new char[] { '��', '��' });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") //TextBoxĭ�� ������ ���� ���
            {
                MessageBox.Show("�ؾ��� ���� ��� �Է��ϼ���.", "����", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            
            string isSuccess; //����(�Ϸ�, ����) ǥ��
            if (checkBox1.Checked == true) { isSuccess = "�Ϸ�"; }
            else { isSuccess = "����"; }

            string todo_str = textBox1.Text;
            string start_date = dateTimePicker1.Text;
            string end_date = dateTimePicker2.Text;

            string[] strArray = new string[]
            {
                isSuccess, todo_str, start_date, end_date
            };
            ListViewItem lvitem = new ListViewItem(strArray); //Listview�� ����(list����) �߰�
            listView1.Items.Add(lvitem);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) { listView1.SelectedItems[0].SubItems[0].Text = "�Ϸ�"; }
            else { listView1.SelectedItems[0].SubItems[0].Text = "����"; }
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
