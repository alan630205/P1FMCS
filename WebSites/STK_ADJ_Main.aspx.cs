using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_ADJ_Main : System.Web.UI.Page
{
    private string m_pk_list = "A";
    //private string M_message = "訊息字串";
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string M_CommText = "";

    //-----查詢用的變數值，查詢時用，當新增取消時，可以用這些變數返回當下畫面
    //private string M_qdepa, M_qserialno, M_qdate, M_qmemo;

    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_STK_PKDI_cmd = new SqlCommand();
    private SqlCommand M_STK_CHEMDATA_cmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    private DataSet M_dd = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
                AddDefaultFirstRecord();
        }
    }
    protected void Btn_query_Click(object sender, EventArgs e)
    {
        DDL_depa.Enabled = true;
        txb_pk_date.Enabled = true;
        txb_PK_serialno.Enabled = true;
        txb_pk_memo.Enabled = true;
        Btn_add.Enabled = false;
        Btn_del.Enabled = false;
        Btn_ok.Enabled = true;
        Btn_Cancel.Enabled = true;
    }

    private void load_si_data()
    {
        M_CommText = "SELECT * FROM STK_PKLIST_SI WHERE PK_LIST='"+m_pk_list+"' AND DEPA='"+DDL_depa.SelectedItem.Value+"' ";
        if (!string.IsNullOrEmpty(txb_PK_serialno.Text)) M_CommText = M_CommText + " AND PK_SERIALNO='" + txb_PK_serialno.Text + "'";
        if (!string.IsNullOrEmpty(txb_pk_date.Text)) M_CommText = M_CommText + " AND PK_DATE='" + txb_pk_date.Text + "'";
        if (!string.IsNullOrEmpty(txb_pk_memo.Text)) M_CommText = M_CommText + " AND PK_MEMO Like '" + txb_pk_memo.Text + "%' ";
        M_CommText = M_CommText + "  order by pk_date desc,pk_serialno asc ";

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.CommandText = M_CommText;
            M_da.SelectCommand = M_sqlcmd;            //da選擇資料來源，由cmd載入進來
            M_da.Fill(M_ds,"DT_STK_PKSI");            //da把資料填入ds裡面

            GridView1.DataSource = M_ds.Tables["DT_STK_PKSI"];
            GridView1.DataBind();
        }
    }
    protected void Btn_ok_Click(object sender, EventArgs e)
    {
        if (Lab_SYS_STATUS.Text == "查修")
        {
            Btn_add.Enabled = true;
            Btn_query.Enabled = true;
            Btn_del.Enabled = true;
            DDL_depa.Enabled = false;
            txb_pk_date.Enabled = false;
            txb_pk_memo.Enabled = false;
            txb_PK_serialno.Enabled = false;
            load_si_data();
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string m_depa="";
        if (Lab_SYS_STATUS.Text=="查修")
        {
            GridViewRow row = GridView1.SelectedRow;
            m_depa = row.Cells[0].Text;
            txb_pk_date.Text = row.Cells[1].Text;

            txb_PK_serialno.Text = GridView1.SelectedDataKey.Value.ToString();

            txb_pk_memo.Text = row.Cells[3].Text;
            lab_qty.Text = row.Cells[4].Text;
            Lab_Create_User.Text = row.Cells[5].Text;

            Lab_Create_DateTime.Text = string.Format("{0:yyyy/MM/dd HH:mm:ss}",  row.Cells[6].Text);
            Lab_Modi_User.Text = row.Cells[7].Text;
            Lab_Modi_DateTime.Text = string.Format("{0:yyyy/MM/dd HH:mm:ss}", row.Cells[8].Text);

            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_STK_PKDI_cmd.Connection = M_SQLConn;
                M_STK_PKDI_cmd.CommandText = "Select a.*,b.partno,b.vendorbatch from STK_Pklist_DI as a left join stk_stock as b on a.depa=b.depa and a.pk_barcode=b.barcode where a.depa='" + m_depa + "'and a.PK_LIST='" + m_pk_list + "' and a.PK_Serialno='" + txb_PK_serialno.Text + "' order by PK_Seq asc";
                M_da.SelectCommand = M_STK_PKDI_cmd;
                M_da.Fill(M_ds, "DT_STK_PKDI");
                GridView2.DataSource = M_ds.Tables["DT_STK_PKDI"];
                GridView2.DataBind();
          }
        }

    }
    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        if (Lab_SYS_STATUS.Text=="查修")
        {
            Btn_ok.Enabled = false;
            Btn_Cancel.Enabled = false;

            txb_pk_date.Enabled = false;
            txb_pk_memo.Enabled = false;
            txb_PK_serialno.Enabled = false;
            DDL_depa.Enabled = false;
        }
        else if (Lab_SYS_STATUS.Text == "新增")
        {
            txb_pk_date.Enabled = false;
            txb_pk_memo.Enabled = false;
            DDL_depa.Enabled = false;
            Btn_add.Enabled = true;
            Btn_del.Enabled = true;
            Btn_query.Enabled = true;
            Btn_ok.Text = "確定";
            Lab_SYS_STATUS.Text = "查修";
            Btn_ok.Enabled = false;
            Btn_Cancel.Enabled = false;

        }

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
        }           
    }
    protected void Btn_add_Click(object sender, EventArgs e)
    {
        //---將表頭欄位 enable 可以編輯，並清為空白
        txb_PK_serialno.Text = "";
        txb_pk_date.Text = string.Format("{0:yyyy/MM/dd}",System.DateTime.Now );
        txb_pk_memo.Text = "";
        Lab_Create_DateTime.Text = string.Format("{0:yyyy/MM/dd HH:mm:ss}", System.DateTime.Now);
        Lab_Create_User.Text = Session["LoginName"].ToString();
        lab_qty.Text = "";
        txb_pk_date.Enabled = true;
        txb_pk_memo.Enabled = true;
        DDL_depa.Enabled = true;
        //------將按鈕改變更為存檔及取消 可以動作，其他變為disable
        Btn_add.Enabled = false;
        Btn_del.Enabled = false;
        Btn_query.Enabled = false;
        Btn_ok.Text = "存檔";
        Btn_ok.Enabled = true;
        Btn_Cancel.Enabled = true;

        Lab_SYS_STATUS.Text = "新增";
        //-----表頭.表身 gridview 先放入一筆空白紀錄，以顯示出表頭
        AddDefaultFirstRecord();

    }

    private void AddDefaultFirstRecord()
    {
        //creating dataTable   建立表頭一筆空白資料
        DataTable dt1 = new DataTable();
        DataRow dr1;
        dt1.TableName = "T_PKLIST_SI";
        dt1.Columns.Add(new DataColumn("depa", typeof(string)));
        dt1.Columns.Add(new DataColumn("PK_Serialno", typeof(string)));
        dt1.Columns.Add(new DataColumn("PK_Date", typeof(string)));
        dt1.Columns.Add(new DataColumn("PK_List", typeof(string)));
        dt1.Columns.Add(new DataColumn("PK_Memo", typeof(string)));
        dt1.Columns.Add(new DataColumn("PK_Qty", typeof(decimal)));
        dt1.Columns.Add(new DataColumn("PK_Create_User", typeof(string)));
        dt1.Columns.Add(new DataColumn("PK_Create_DateTime", typeof(DateTime)));
        dt1.Columns.Add(new DataColumn("PK_Modi_User", typeof(string)));
        dt1.Columns.Add(new DataColumn("PK_Modi_DateTime", typeof(DateTime)));

        dr1 = dt1.NewRow();
        dt1.Rows.Add(dr1);
        //saving databale into viewstate   
        ViewState["T_PKLIST_SI"] = dt1;
        //bind Gridview  
        GridView1.DataSource = dt1;
        GridView1.DataBind();

        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_PKLIST_DI";
        dt2.Columns.Add(new DataColumn("depa", typeof(string)));
        dt2.Columns.Add(new DataColumn("PK_List", typeof(string)));
        dt2.Columns.Add(new DataColumn("PK_Serialno", typeof(string)));
        dt2.Columns.Add(new DataColumn("PK_Seq", typeof(int)));
        dt2.Columns.Add(new DataColumn("PK_Barcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("Partno", typeof(string)));
        dt2.Columns.Add(new DataColumn("Vendorbatch", typeof(string)));
        dt2.Columns.Add(new DataColumn("PK_Qty", typeof(decimal)));
        dr2 = dt2.NewRow();
        dt2.Rows.Add(dr2);
        //saving databale into viewstate   
        ViewState["T_PKLIST_DI"] = dt2;
        //bind Gridview  
        GridView2.DataSource = dt2;
        GridView2.DataBind();
    }

    /*
    protected void btn_add_detail_Click(object sender, EventArgs e)
    {

        //將 txb_input_Barcode 轉入 gridview2
        if (ViewState["T_PKLIST_DI"] != null & txb_input_barcode.Text!=""   )
        {
            DataTable dtCurrentTable = (DataTable)ViewState["T_PKLIST_DI"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["depa"] = DDL_depa.Text;
                    drCurrentRow["PK_Serialno"] = txb_PK_serialno.Text;
                    drCurrentRow["PK_Seq"] = i;
                    drCurrentRow["PK_Barcode"] = txb_input_barcode.Text;
                    drCurrentRow["PK_Qty"] = 1;
                }

                lab_qty.Text = Convert.ToString(dtCurrentTable.Rows.Count);

                if (dtCurrentTable.Rows[0][0].ToString() == "")
                {
                    dtCurrentTable.Rows[0].Delete();
                    dtCurrentTable.AcceptChanges();
                }
                else
                {
                  if (dtCurrentTable.Rows.Count>=1)
                  drCurrentRow["PK_Seq"] = Convert.ToInt32(dtCurrentTable.Rows[dtCurrentTable.Rows.Count - 1]["PK_Seq"].ToString()) + 1; 
                }

                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["T_PKLIST_DI"] = dtCurrentTable;
                GridView2.DataSource = dtCurrentTable;
                GridView2.DataBind();
            }
        }
        txb_input_barcode.Text = "";
        txb_input_barcode.Focus();
    }
    */

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Lab_SYS_STATUS.Text=="新增")
        {

        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        GV2_GetData();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        GV2_GetData();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        /*
        string strdepa, strchemname;

        strdepa = ((Label)GridView2.Rows[e.RowIndex].Cells[0].
            FindControl("lbldepa")).Text;

        strchemname = ((Label)GridView2.Rows[e.RowIndex].Cells[0].
            FindControl("lblchemname")).Text;

        
        M_sqlcmd.Parameters.Clear();
        M_sqlcmd.Parameters.AddWithValue("depa", strdepa);
        M_sqlcmd.Parameters.AddWithValue("chemname", strchemname);

        M_sqlcmd.CommandText = "DELETE FROM STK_Chemdata WHERE depa=@depa and chemname=@chemname ";
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.ExecuteNonQuery();
        }
        */
        GV2_GetData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        GV2_GetData();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string m_pk_serial,m_pk_list,m_pk_barcode,m_partno,m_vendorbatch;
        int m_pk_seq,m_reccount;
        decimal m_pk_qty;
        string m_errorobject = "";

        ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('TEST.....');</script>");

        try
        {
            m_pk_serial = txb_PK_serialno.Text;
            m_pk_seq = int.Parse( ((Label)GridView2.Rows[e.RowIndex].Cells[0].FindControl("lblpk_seqEdit")).Text );
            m_pk_barcode = ((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbpk_barcodeEdit")).Text;
            m_errorobject = "數量輸入格式";
            m_pk_qty = decimal.Parse(((TextBox)GridView2.Rows[e.RowIndex].Cells[0].FindControl("tbpk_qtyEdit")).Text);
            m_errorobject = "";
            //--barcode 拆解 partno,vendorbatch
            using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
            {
                M_STK_CHEMDATA_cmd.Connection = M_SQLConn;
                M_STK_CHEMDATA_cmd.CommandText = "Select * from STK_CHEMDATA where depa='" + DDL_depa.SelectedValue.ToString() + "' order by PK_Seq asc";
                M_da.SelectCommand = M_STK_CHEMDATA_cmd;
                M_da.Fill(M_ds, "DT_STK_CHEMDATA");
            }
            m_reccount = M_ds.Tables["DT_STK_CHEMDATA"].Rows.Count;
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_reccount.ToString() + "');</script>");


        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('" + m_errorobject + ex.Message + "');</script>");
            return;
        }

        /*

        M_sqlcmd.Parameters.Clear();
        M_sqlcmd.Parameters.AddWithValue("depa", strdepa);
        M_sqlcmd.Parameters.AddWithValue("chemname", strchemName);
        M_sqlcmd.Parameters.AddWithValue("drumcode", strdrumcode);

        M_sqlcmd.CommandText =
            @"UPDATE stk_chemdata SET drumcode=@drumcode " +
            @"WHERE depa=@depa and chemname=@chemname ";

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_SQLConn.Open();
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.ExecuteNonQuery();
            M_SQLConn.Close();
        }
        */
        GridView2.EditIndex = -1;

        GV2_GetData();
    }
    protected void lbInsert_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = true;
    }
    protected void lbSave_Click(object sender, EventArgs e)
    {
        string strdepa, strchemname, strdrumcode;

        strdepa = ((TextBox)GridView2.FooterRow.Cells[0].
            FindControl("tbpk_seqFooter")).Text;
        strchemname = ((TextBox)GridView2.FooterRow.Cells[0].
            FindControl("tbpk_barcodeFooter")).Text;
        strdrumcode = ((TextBox)GridView2.FooterRow.Cells[0].
            FindControl("tbpk_qtyFooter")).Text;


        /* 新增資料驗證作業 
           ...
           ...
           ...
        */

        /* 更新資料 */
        M_sqlcmd.Parameters.Clear();
        M_sqlcmd.Parameters.AddWithValue("depa", strdepa);
        M_sqlcmd.Parameters.AddWithValue("chemname", strchemname);
        M_sqlcmd.Parameters.AddWithValue("drumcode", strdrumcode);

        M_sqlcmd.CommandText =
            @"INSERT INTO stk_chemdata (depa, chemname, " +
            @"drumcode  )" +
            @" VALUES (@depa,@chemname,@drumcode ) ";

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.ExecuteNonQuery();
        }

        GV2_GetData();
    }
    protected void lbCancelSave_Click(object sender, EventArgs e)
    {
        GridView2.FooterRow.Visible = false;
    }

    protected void GV2_GetData()
    {
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_STK_PKDI_cmd.Connection = M_SQLConn;
            M_STK_PKDI_cmd.CommandText = "Select a.*,b.partno,b.vendorbatch from STK_Pklist_DI as a left join stk_stock as b on a.depa=b.depa and a.pk_barcode=b.barcode where a.depa='" + DDL_depa.SelectedValue.ToString() + "' and a.pk_list='"+m_pk_list+"' and a.PK_Serialno='" + txb_PK_serialno.Text + "' order by PK_Seq asc";
            M_da.SelectCommand = M_STK_PKDI_cmd;
            M_da.Fill(M_ds, "DT_STK_PKDI");
            GridView2.DataSource = M_ds.Tables["DT_STK_PKDI"];
            GridView2.DataBind();
        }
    }
}