using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BluetoothAuscultation.ExtendControl;
using ProtocalData.Protocol.Derive;
using ProtocalData.Utilities;
using ProtocalData.Protocol;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.IO;

namespace BluetoothAuscultation.Forms
{
    public partial class FormAdmin :   Form
        
    {
        public FormAdmin()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormAdmin_Load);
        }
        string serVerAudioPath = System.Configuration.ConfigurationManager.AppSettings["AudioFilePath"];
        string tableName = string.Empty;
        void FormAdmin_Load(object sender, EventArgs e)
        {
            Load_dgvAuthInfo();
            tableName = "AccountAuthCustomInfo";
            LoadCols();
            var root = new TreeNode(Path.GetFileName(serVerAudioPath));
            root.Nodes.Add(new TreeNode("test"));
            tvFileInfo.Nodes.Add(root);
            btnSearch.Enabled = false;
            cmbCol.Enabled = false;
            txtValue.Enabled = false;
        }
        string [] GetColsByTableName(string tableName)
        {
            string  sql=" select name from sys.columns where object_id=object_id({0})";
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { tableName });
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Select().Select(r => r[0] + "").ToArray();
                }
                return null;
            }
        }
        void LoadCols()
        {
            cmbCol.Items.Clear();
            cmbCol.Items.AddRange(GetColsByTableName(this.tableName));
        }

        private void tabManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = true;
            cmbCol.Enabled = true;
            txtValue.Enabled = true;
            cmbCol.Text = "";
            txtValue.Text = "";
            switch (tabManager.SelectedTab.Name)
            {
                case "tpAccountSet":
                    btnSearch.Enabled = false;
                    cmbCol.Enabled = false;
                    txtValue.Enabled = false;
                    btnInsert.Enabled = true;
                    tableName = "AccountAuthCustomInfo";
                    Load_dgvAuthInfo();
                    LoadCols();
                    break;
                case "tpAuthRegist":
                    btnInsert.Enabled = false;
                    tableName = "View_ActiveAccount";
                    LoaddgvActiveAccount();
                    LoadCols();
                    break;
                case "tpOnline":
                    btnInsert.Enabled = false;
                    tableName = "View_OnlineUser";
                    LoaddgvOnline();
                    LoadCols();
                    break;
                case "tpSerRunLog":
                    btnInsert.Enabled = false;
                    tableName = "View_SerRunLog";
                    LoaddgvSerRunLog();
                    LoadCols();
                    break;
                case "tpFileManager":
                    btnInsert.Enabled = false;
                    tableName = "View_FileInfo";
                    LoadFileInfo();
                    LoadCols();
                    break;
                case "tpDownHis":
                    btnInsert.Enabled = false;
                    tableName = "View_DownHis";
                    LoadDownHis();
                    LoadCols();
                    break;
                case "tpLoginLog":
                    btnInsert.Enabled = false;
                    tableName = "View_LoginLog";
                    LoadLoginLog();
                    LoadCols();
                    break;
                case "tpShare":
                    btnInsert.Enabled = false;
                    tableName = "View_ShareInfo";
                    LoadShareInfo();
                    LoadCols();
                    break;
                case "tpClientOper":
                    btnInsert.Enabled = false;
                    tableName = "View_ClientOper";
                    LoadClientOper();
                    LoadCols();
                    break;

            }
        }
        void LoadClientOper()
        {
            string sql = string.Format(@"select * from {0} where 1=1 ", this.tableName) + filter;
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvClientOper.DataSource = ds.Tables[0];
                }
            }
        }
         void LoadShareInfo()
         {
             
            string sql = string.Format(@"select * from {0} where 1=1 ", this.tableName) + filter;
             using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
             {
                 MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                 OperationContext.Current.OutgoingMessageHeaders.Add(header);
                 header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                 OperationContext.Current.OutgoingMessageHeaders.Add(header);
                 var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                 if (ds != null && ds.Tables.Count > 0)
                 {
                     dgvShare.DataSource = ds.Tables[0];
                 }
             }
         }
        void LoadLoginLog()
        {
            
            string sql = string.Format(@"select * from {0} where 1=1 ", this.tableName) + filter;
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvLoginLog.DataSource = ds.Tables[0];
                }
            }
        }
        

    
        void LoadDownHis()
        {
            
            string sql = string.Format(@"select * from {0} where 1=1 ", this.tableName) + filter;
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvFileDown.DataSource = ds.Tables[0];
                }
            }
        }
        void LoadFileInfo()
        {
             string sql = string.Format(@"select * from {0} where 1=1 ", this.tableName) + filter;
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvFileInfo.DataSource = ds.Tables[0];
                }
            }
        }
        void Load_dgvAuthInfo()
        {
            dgvAuthInfo.Rows.Clear();
            string sql="select * from AccountAuthCustomInfo where isDel=0";
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dgvAuthInfo.Rows.Add(
                            null,
                            null,
                            row[0],
                            row[1],
                            row[2],
                            row[3],
                            row[4],
                            row[5],
                            row[6],
                            row[7],
                            row[8],
                            row[9],
                            row[10],
                            row[11],
                            row[12],
                            row[13]
                            );
                    }
                }
            }
           
        }
        string SN = Mediator.SN;
        string MAC = Mediator.MAC;

        void LoaddgvSerRunLog()
        { 
            string sql = string.Format(@"select * from {0} where 1=1 ", this.tableName) + filter;
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[]{});
                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvSRL.DataSource = ds.Tables[0];
                }
            }
        }
        void InsertdgvAuthInfo()
        {
            string sql = @" insert into [AccountAuthCustomInfo]([AuthorizationNum]) select NEWID();
    select * from [AccountAuthCustomInfo] where ID=@@IDENTITY";
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dgvAuthInfo.Rows.Add(
                            null,
                            null,
                            row[0],
                            row[1],
                            row[2],
                            row[3],
                            row[4],
                            row[5],
                            row[6],
                            row[7],
                            row[8],
                            row[9],
                            row[10],
                            row[11],
                            row[12],
                            row[13]
                            );
                    }

                }
            }
        }
        int DeletedgvAuthInfo(int ID)
        {
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                //string sql = "delete [AccountAuthCustomInfo] where ID={0}";
                 string sql = "update [AccountAuthCustomInfo] set isDel=1 where ID={0}";
                return Mediator.remoteService.ExecuteNonQuery(sql, new string[] { ID.ToString() });
            }
        }
        private void dgvAuthInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                switch (dgvAuthInfo.Columns[e.ColumnIndex].Name)
                {
                    case "dgvAuthInfo_Edit":
                        {

                            var formAuthInfo = new FormAuthInfo()
                                {

                                    AuthorizationNum = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_SN"].Value.ToString(),
                                    AuthDays = int.Parse(dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_AuthDays"].Value.ToString()),
                                    BindCount = int.Parse(dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_BindCount"].Value.ToString()),
                                    GroupID = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_GroupID"].Value.ToString(),
                                    GroupName = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_GroupName"].Value.ToString(),
                                    GroupRemark = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_Remark"].Value.ToString(),
                                    CustomName = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_CoustomName"].Value.ToString(),
                                    CustomEmail = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_Email"].Value.ToString(),
                                    CustomTel = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_Tel"].Value.ToString(),
                                    CustomAddress = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_Address"].Value.ToString(),
                                    CustomReamrk = dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_BeiZhu"].Value.ToString(),

                                };

                            if (DialogResult.OK == formAuthInfo.ShowDialog())
                            {
                                string sql = @"  update AccountAuthCustomInfo set GroupID={0},GroupName={1},GroupReamrk={2},AuthDays={3},BindCount={4},
  CustomName={5},Email={6},Tel={7},Address={8},Remark={9} where AuthorizationNum={10}";
                                if (Mediator.remoteService.ExecuteNonQuery(sql, new string[] { formAuthInfo.GroupID.ToString(), formAuthInfo.GroupName, formAuthInfo.GroupRemark, formAuthInfo.AuthDays.ToString(), formAuthInfo.BindCount.ToString()
                            ,formAuthInfo.CustomName,formAuthInfo.CustomEmail,formAuthInfo.CustomTel,formAuthInfo.CustomAddress,formAuthInfo.CustomReamrk,formAuthInfo.AuthorizationNum}) > 0)
                                {
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_SN"].Value = formAuthInfo.AuthorizationNum;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_AuthDays"].Value = formAuthInfo.AuthDays;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_BindCount"].Value = formAuthInfo.BindCount;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_GroupID"].Value = formAuthInfo.GroupID;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_GroupName"].Value = formAuthInfo.GroupName;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_Remark"].Value = formAuthInfo.GroupRemark;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_CoustomName"].Value = formAuthInfo.CustomName;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_Email"].Value = formAuthInfo.CustomEmail;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_Tel"].Value = formAuthInfo.CustomTel;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_Address"].Value = formAuthInfo.CustomAddress;
                                    dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_BeiZhu"].Value = formAuthInfo.CustomReamrk;

                                }
                            }
                        }
                        break;
                    case "dgvAuthInfo_Delete":
                        {
                            if (DialogResult.OK == MessageBox.Show("确定要删除此记录?", "删除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                            {
                                var count = DeletedgvAuthInfo(int.Parse(dgvAuthInfo.Rows[e.RowIndex].Cells["dgvAuthInfo_ID"].Value.ToString()));
                                if (count > 0)
                                {
                                    dgvAuthInfo.Rows.RemoveAt(e.RowIndex);
                                }
                            }
                        }
                        break;
                }
            }
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            
            if (tabManager.SelectedTab != null)
            {
                switch (tabManager.SelectedTab.Name)
                {
                    case "tpAccountSet":
                        InsertdgvAuthInfo();
                        break;
                }
            }
        }
        

        string filter
        {
            get
            {
                string colName = cmbCol.Text;
                string oper = cmbOper.Text;
                string value = txtValue.Text;
                if (!string.IsNullOrEmpty(colName) && !string.IsNullOrEmpty(oper) && !string.IsNullOrEmpty(value))
                {
                    return " and "+colName + oper + "'" + value + "'";
                }
                return string.Empty;
            }
        }

        void LoaddgvActiveAccount()
        {
            string sql = string.Format(@"select * from {0} where 1=1 ", this.tableName) + filter;
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        dgvActiveAccount.DataSource = ds.Tables[0];
                    }
                }
            }
        }
        void LoaddgvOnline()
        {
            string sql = string.Format(@"select * from {0} where 1=1 ", this.tableName) + filter;
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] { });
                if (ds != null && ds.Tables.Count > 0)
                {
                    dgvOnline.DataSource = ds.Tables[0];
                }
            }
           
        }

       

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (tabManager.SelectedTab != null)
            {
                switch (tabManager.SelectedTab.Name)
                {

                    case "tpAccountSet":
                        Load_dgvAuthInfo();
                        break;
                    case "tpAuthRegist":
                        LoaddgvActiveAccount();
                        break;
                    case "tpOnline":
                        LoaddgvOnline();
                        break;
                    case "tpSerRunLog":
                        LoaddgvSerRunLog();
                        break;
                    case "tpFileManager":
                        LoadFileInfo();
                        break;
                    case "tpDownHis":
                        LoadDownHis();
                        break;
                    case "tpLoginLog":
                        LoadLoginLog();
                        break;
                    case "tpShare":
                        LoadShareInfo();
                        break;
                    case "tpClientOper":
                        LoadClientOper();
                        break;
                }
            }
        }

        private void tvFileInfo_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            var path = e.Node.FullPath;
            var fullPath = Path.Combine(Path.GetDirectoryName(serVerAudioPath), path);
            var childDir = Directory.GetDirectories(fullPath);
            if (childDir.Length > 0)
                foreach (var dir in childDir)
                {
                    var node = new TreeNode(Path.GetFileName(dir));
                    node.Nodes.Add(new TreeNode("test"));
                    e.Node.Nodes.Add(node);
                }
            var childFile = Directory.GetFiles(fullPath);
            if (childFile.Length > 0)
                foreach (var file in childFile)
                {
                    var node = new TreeNode(Path.GetFileName(file));
                    e.Node.Nodes.Add(node);
                }
        }

        private void tvFileInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
             
        }
        void LoadTxtValues()
        {
            this.txtValue.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.txtValue.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            string sql = string.Format(" select distinct {0} from {1} ",cmbCol.Text,this.tableName);
            using (OperationContextScope scope = new OperationContextScope(Mediator.remoteService.InnerChannel))
            {
                MessageHeader header = MessageHeader.CreateHeader("SN", "http://tempuri.org", SN);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                header = MessageHeader.CreateHeader("MAC", "http://tempuri.org", MAC);
                OperationContext.Current.OutgoingMessageHeaders.Add(header);
                var ds = Mediator.remoteService.ExecuteDataset(sql, new string[] {  });
                if (ds != null && ds.Tables.Count > 0)
                {
                    coll.AddRange(ds.Tables[0].Select().Select(r => r[0] + "").ToArray());
                }
            }
            this.txtValue.AutoCompleteCustomSource = coll;
        }
        private void cmbCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbCol.SelectedIndex>=0)
            {
                LoadTxtValues();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnUpdate_Click(null, null);
        }
    }
}
