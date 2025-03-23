using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VNPT.HIS.UserControl
{
    public partial class ucPage : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EventHandler page_change;
        public ucPage()
        {
            InitializeComponent();            
        }

        private void ucPage_Load(object sender, EventArgs e)
        {

        }
        private void Init()
        {
            btnBack.Enabled = false;
            btnFirst.Enabled = false;

            btnNext.Enabled = true;
            btnLast.Enabled = true;
        }
        public void setEvent(EventHandler _getData)
        {
            page_change = _getData;
        }
        private int totalPage = 0;
        public void setData(int total, int currentPage, int records)
        {
            try
            {
                totalPage = total;
                lbTotal.Text = "/" + totalPage;

                if (records > 0)
                {
                    panelControl1.Visible = true;
                    int start = (currentPage - 1) * getNumberPerPage() + 1;
                    int end = currentPage * getNumberPerPage();
                    if (end > records) end = records;
                    lb_TongBanGhi.Text = start.ToString() + " đến " + end.ToString() + " / " + records;
                    //if (lb_TongBanGhi.Size.Width > panelControl1.Size.Width)
                        panelControl1.Size = new Size(lb_TongBanGhi.Size.Width+3, lb_TongBanGhi.Size.Height);
                }
                else panelControl1.Visible = false;

                //Init();
                
                cboPage.SelectedIndexChanged -= cboPage_SelectedIndexChanged;
                if (totalPage != cboPage.Properties.Items.Count)
                { 
                    cboPage.Properties.Items.Clear();
                    for (int i = 1; i <= totalPage; i++)
                        cboPage.Properties.Items.Add(i); 
                }
                cboPage.SelectedIndex = currentPage - 1;
                cboPage.SelectedIndexChanged += cboPage_SelectedIndexChanged;

                enableButton();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        private int numberBefore = 20;
        public int getNumberPerPage()
        {
            int number = 0;
            try
            {
                number = Convert.ToInt32(cboHienThi.Text);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return number;
        }
        public void setNumberPerPage(int[] listNumber)
        {
            setNumberPerPage(listNumber, 0);
        }
        public void setNumberPerPage(int[] listNumber, int indexDefault)
        {
            cboHienThi.Properties.Items.Clear();
            cboHienThi.Text = "";

            for (int i = 0; i < listNumber.Length; i++)
                cboHienThi.Properties.Items.Add(listNumber[i]);
            if (listNumber.Length > 0 && listNumber.Length > indexDefault) cboHienThi.SelectedIndex = indexDefault;
        }
        public int Current()
        {
            int temp = VNPT.HIS.Common.Func.Parse(cboPage.Text);
            if (temp <= 0) temp = 1;
            return temp;
        }
        
        private int getNextPage(int step)
        {
            try
            {
                int page = 0;
                try
                {
                    page = Convert.ToInt32(cboPage.Text) + step;
                }
                catch (Exception ex) { log.Fatal(ex.ToString()); }

                
                if (page <= cboPage.Properties.Items.Count && page > 0)
                {
                    cboPage.SelectedIndex = page-1;
                    return page;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return 0;
        }
        private void enableButton()
        {
            btnBack.Enabled = Current() != 1; //
            btnFirst.Enabled = Current() != 1;

            btnNext.Enabled = Current() != totalPage;
            btnLast.Enabled = Current() != totalPage;
        }
        private void cboPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (page_change!=null) page_change(Current(), null);

            enableButton();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            getNextPage(1);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            getNextPage(-1);
        }

        private void cboHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            int here = numberBefore * (Current() - 1);

            numberBefore = getNumberPerPage();

            int newPage = here / numberBefore + 1;

            if (page_change != null) page_change(newPage, null);
            enableButton();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (cboPage.SelectedIndex != 0) cboPage.SelectedIndex = 0;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (cboPage.SelectedIndex != totalPage-1) cboPage.SelectedIndex = totalPage-1; 
        }

    }
}
