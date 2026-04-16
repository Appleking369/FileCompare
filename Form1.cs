namespace FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                // 현재 텍스트박스에 있는 경로를 초기 선택 폴더로 설정
                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) &&
                    Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                // 현재 텍스트박스에 있는 경로를 초기 선택 폴더로 설정
                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) &&
                    Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {

        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {

        }

        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            try
            {
                // 폴더(디렉터리) 먼저 추가
                var dirs = Directory.EnumerateDirectories(folderPath)
                    .Select(p => new DirectoryInfo(p))
                    .OrderBy(d => d.Name);

                foreach (var d in dirs)
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(d.LastWriteTime.ToString("g"));
                    lv.Items.Add(item);

                    // 파일 추가
                    var files = Directory.EnumerateFiles(folderPath)
                                         .Select(p => new FileInfo(p))
                                         .OrderBy(f => f.Name);

                    foreach (var f in files)
                    {
                        ListViewItem listViewItem = new(f.Name);
                        listViewItem.SubItems.Add(f.Length.ToString("N0") + " 바이트");
                        listViewItem.SubItems.Add(f.LastWriteTime.ToString("g"));
                        lv.Items.Add(listViewItem);

                        // 컬럼 너비 자동 조정(컨텐츠 기준)
                        for (int i = 0; i < lv.Columns.Count; i++)
                        {
                            lv.AutoResizeColumn(i,
                                ColumnHeaderAutoResizeStyle.ColumnContent);
                        }
                    }
                }
            }
            finally
            {
                lv.EndUpdate();
            }
        }
    }
}
