using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_PartChk_Edit : System.Web.UI.Page
{

    //private string M_message = "訊息字串";
    private string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    private string[] M_appconnstr = ConfigurationManager.AppSettings.GetValues("gStrConn");

    private string M_CommText = "";

    //-----查詢用的變數值，查詢時用，當新增取消時，可以用這些變數返回當下畫面
    //private string M_qdepa, M_qserialno, M_qdate, M_qmemo;

    private SqlCommand m_update_cmd = new SqlCommand();
    private SqlCommand M_sqlcmd = new SqlCommand();
    private SqlCommand M_STK_PKDI_cmd = new SqlCommand();
    private SqlCommand M_STK_CHEMDATA_cmd = new SqlCommand();
    private SqlDataAdapter M_da = new SqlDataAdapter();
    private DataSet M_ds = new DataSet();
    private DataSet M_dd = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        string m_script, m_url;
        m_url = "STK_Calendar.aspx?TextBoxId=" + txb_date.ClientID;
        m_script = "window.open('" + m_url + "','','height=315,width=350,status=no,toolbar=no,menubar=no,location=no','')";
        if (!IsPostBack)
        {
            txb_date.Text = DateTime.Now.ToString("yyyy/MM/dd");
            GET_DDL_DATA_WS();
            //DropDownList1_SelectedIndexChanged(this, new EventArgs());
            AddDefaultFirstRecord();
        }
        Button1.OnClientClick = m_script;
    }

    private void load_si_data()
    {
        M_CommText = "SELECT * FROM STK_PART_CHK_SI WHERE chk_ws='" + DDL_WS.SelectedItem.Value + "' ";
        M_CommText = M_CommText + " AND CHK_DATE ='" + txb_date.Text + "' ";
        M_CommText = M_CommText + "  order by chk_date,chk_ws,chemname asc ";

        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_sqlcmd.Connection = M_SQLConn;
            M_sqlcmd.CommandText = M_CommText;
            M_da.SelectCommand = M_sqlcmd;            //da選擇資料來源，由cmd載入進來
            M_da.Fill(M_ds, "DT_STK_PART_CHK_SI");            //da把資料填入ds裡面

            GridView1.DataSource = M_ds.Tables["DT_STK_PART_CHK_SI"];
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位  
            if (Session["LoginName"] == null || !Chk_Authority(Session["LoginName"].ToString(), "STK_PARTCHK_EDIT"))
            {
                e.Row.Cells[0].Visible = false;
            }
        }
    }
    protected bool Chk_Authority(string m_loginid, string m_appid)
    {
        bool m_result = false;
        SqlCommand M_Chk_Authority_cmd = new SqlCommand();

        using (SqlConnection M_SQLConn = new SqlConnection(M_appconnstr[0]))
        {
            M_Chk_Authority_cmd.Connection = M_SQLConn;
            M_SQLConn.Open();

            M_Chk_Authority_cmd.CommandText = "Select top 1 Write_Flag from User_Task where user_id=@m_loginid and Task_ID=@m_appid";
            M_Chk_Authority_cmd.Parameters.Clear();
            M_Chk_Authority_cmd.Parameters.AddWithValue("m_loginid", m_loginid);
            M_Chk_Authority_cmd.Parameters.AddWithValue("m_appid", m_appid);

            SqlDataReader M_dr = M_Chk_Authority_cmd.ExecuteReader();

            if (M_dr.HasRows)
            {
                M_dr.Read();
                m_result = Convert.ToBoolean(M_dr["Write_Flag"].ToString());
            }
            else
            {
                m_result = false;
            }
            M_dr.Close();
            M_dr.Dispose();
            M_SQLConn.Close();
            M_SQLConn.Dispose();
        }
        M_Chk_Authority_cmd.Dispose();
        return m_result;
    }


    private void AddDefaultFirstRecord()
    {
        //creating dataTable   建立表頭一筆空白資料
        DataTable dt1 = new DataTable();
        DataRow dr1;
        dt1.TableName = "T_STK_PART_CHK_SI";
        dt1.Columns.Add(new DataColumn("chk_date", typeof(string)));
        dt1.Columns.Add(new DataColumn("chk_ws", typeof(string)));
        dt1.Columns.Add(new DataColumn("chk_drumcode", typeof(string)));
        dt1.Columns.Add(new DataColumn("chemname", typeof(string)));
        dt1.Columns.Add(new DataColumn("stk_qty", typeof(decimal)));
        dt1.Columns.Add(new DataColumn("keyin_qty", typeof(decimal)));
        dt1.Columns.Add(new DataColumn("chk_qty", typeof(decimal)));
        dt1.Columns.Add(new DataColumn("adjust_ok", typeof(bool)));
        dt1.Columns.Add(new DataColumn("userid", typeof(string)));
        dt1.Columns.Add(new DataColumn("rectime", typeof(DateTime)));
        
        dr1 = dt1.NewRow();
        dr1["adjust_ok"] = 0;
        dt1.Rows.Add(dr1);
        ViewState["T_STK_PART_CHK_SI"] = dt1;
        GridView1.DataSource = dt1;
        GridView1.DataBind();

        //-----
        //creating dataTable   建立表身一筆空白資料
        DataTable dt2 = new DataTable();
        DataRow dr2;
        dt2.TableName = "T_STK_PART_CHK_DI";
        dt2.Columns.Add(new DataColumn("chk_drumcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("chk_vendorbatch", typeof(string)));
        dt2.Columns.Add(new DataColumn("chk_barcode", typeof(string)));
        dt2.Columns.Add(new DataColumn("stk_qty", typeof(int)));
        dt2.Columns.Add(new DataColumn("chk_qty", typeof(string)));
        dr2 = dt2.NewRow();
        dr2["stk_qty"] = 0;
        dr2["chk_qty"] = 0;
        dt2.Rows.Add(dr2);
        ViewState["T_STK_PART_CHK_DI"] = dt2;
        GridView2.DataSource = dt2;
        GridView2.DataBind();
    }

    private void GET_DDL_DATA_WS()
    {
        string m_sqlstr = "SELECT wsno from STK_WS Order By wsno ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DDL_WS.DataValueField = "wsno";
                DDL_WS.DataTextField = "wsno";

                DDL_WS.DataSource = ddl;
                DDL_WS.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message.ToString() + "');</script>");
            }
            finally
            {
                DDL_strCon.Close();
                DDL_strCon.Dispose();
            }
        }
    }

    protected void Btn_Query_Click(object sender, EventArgs e)
    {
        load_si_data();
    }

    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        string m_chk_date = "", m_chk_ws = "", m_chk_drumcode = "";
        
        m_chk_date = GridView1.SelectedDataKey["chk_date"].ToString();
        m_chk_ws = GridView1.SelectedDataKey["chk_ws"].ToString();
        m_chk_drumcode = GridView1.SelectedDataKey["chk_drumcode"].ToString();
        
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_STK_PKDI_cmd.Connection = M_SQLConn;
            M_STK_PKDI_cmd.CommandText = "SELECT * FROM STK_Part_Chk_DI WHERE chk_date='" + m_chk_date + "'and chk_ws='" + m_chk_ws + "' and chk_drumcode='" + m_chk_drumcode + "' order by chk_vendorbatch,chk_barcode asc";
            M_da.SelectCommand = M_STK_PKDI_cmd;
            M_da.Fill(M_ds, "DT_STK_PKDI");
            GridView2.DataSource = M_ds.Tables["DT_STK_PKDI"];
            GridView2.DataBind();
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string m_date,m_ws,m_userid,m_drumcode;
        bool m_adjust_ok;
        string m_update_sql="";

        m_userid = Session["LoginName"].ToString();

        if (e.CommandName=="ADJUST")
        {
            m_date = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)][0].ToString();
            m_drumcode = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)][2].ToString();
            m_ws = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)][1].ToString();
            m_adjust_ok = Convert.ToBoolean(GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)][3]);  
            
            if (m_adjust_ok)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "script", "alert('該筆資料"+m_drumcode+"已經核帳調整完筆，不可重複調整')", true);
            }
            else
            {
                m_update_sql += "insert into STK_ADJ_Log(ws,adj_datetime,barcode,drumcode,vendorbatch,ExpirationDate,adj_qty,adj_desc,adj_userid,data_cancel) ";
                m_update_sql += "Select a.chk_ws,GETDATE() as adj_datetime,a.chk_barcode,a.chk_drumcode,a.chk_vendorbatch,c.ExpirationDate,(a.chk_qty-a.stk_qty) as adj_qty,convert(varchar(10),a.chk_date,120)+' 核帳調整' as adj_desc,@m_userid as adj_userid,0 as data_cancel from STK_Part_Chk_DI as a left join STK_Part_Chk_SI as b on a.chk_date=b.chk_date and a.chk_ws=b.chk_ws and a.chk_drumcode=b.chk_drumcode left join STK_Stock as c on a.chk_ws=c.ws and a.chk_barcode=c.barcode Where b.adjust_ok=0 and a.stk_qty<>a.chk_qty and b.chk_date=@m_chkdate and b.chk_ws=@m_ws and b.chk_drumcode=@m_drumcode; ";
                m_update_sql += "";
                m_update_sql += "update STK_Stock set STK_ADJ_QTY=STK_ADJ_QTY+isnull(b.adj_qty,0),STK_QTY=STK_QTY+isnull(b.adj_qty,0) from stk_stock as a left join ";
                m_update_sql += "(Select c.chk_date,c.chk_ws,c.chk_barcode,(c.chk_qty-c.stk_qty) as adj_qty from stk_part_chk_di as c left join STK_Part_Chk_SI as d on c.chk_date=d.chk_date and c.chk_ws=d.chk_ws and c.chk_drumcode=d.chk_drumcode where d.chk_date=@m_chkdate and d.adjust_ok=0 and d.chk_ws=@m_ws and d.chk_drumcode=@m_drumcode ) as b ";
                m_update_sql += "on a.ws=b.chk_ws and a.barcode=b.chk_barcode where b.chk_barcode is not null ; ";
                m_update_sql += "update STK_Part_Chk_SI set adjust_ok=1 where chk_date=@m_chkdate and chk_ws=@m_ws and chk_drumcode=@m_drumcode ;";

                try
                {

                    m_update_cmd.CommandText = m_update_sql;
                    m_update_cmd.Parameters.Clear();
                    m_update_cmd.Parameters.AddWithValue("m_ws", m_ws);
                    m_update_cmd.Parameters.AddWithValue("m_userid", m_userid);
                    m_update_cmd.Parameters.AddWithValue("m_chkdate", m_date);
                    m_update_cmd.Parameters.AddWithValue("m_drumcode", m_drumcode);

                    using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
                    {
                        M_SQLConn.Open();
                        m_update_cmd.Connection = M_SQLConn;
                        m_update_cmd.ExecuteNonQuery();
                        M_SQLConn.Close();
                    }
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "script", "alert('"+m_drumcode+" 核帳調整完成')", true);

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "script", "alert('" + ex.Message + "')", true);

                    //Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
                finally
                {
                    m_update_cmd.Dispose();
                    load_si_data();
                }
            }

        }
        if (e.CommandName=="ALL_ADJUST")
        {
            m_date = txb_date.Text;

            m_update_sql += "insert into STK_ADJ_Log(ws,adj_datetime,barcode,drumcode,vendorbatch,ExpirationDate,adj_qty,adj_desc,adj_userid,data_cancel) ";
            m_update_sql += "Select a.chk_ws,GETDATE() as adj_datetime,a.chk_barcode,a.chk_drumcode,a.chk_vendorbatch,c.ExpirationDate,(a.chk_qty-a.stk_qty) as adj_qty,convert(varchar(10),a.chk_date,120)+' 核帳調整' as adj_desc,@m_userid as adj_userid,0 as data_cancel from STK_Part_Chk_DI as a left join STK_Part_Chk_SI as b on a.chk_date=b.chk_date and a.chk_ws=b.chk_ws and a.chk_drumcode=b.chk_drumcode left join STK_Stock as c on a.chk_ws=c.ws and a.chk_barcode=c.barcode Where b.adjust_ok=0 and a.stk_qty<>a.chk_qty and b.chk_date=@m_chkdate ; ";
            m_update_sql += "";
            m_update_sql += "update STK_Stock set STK_ADJ_QTY=STK_ADJ_QTY+isnull(b.adj_qty,0),STK_QTY=STK_QTY+isnull(b.adj_qty,0) from stk_stock as a left join ";
            m_update_sql += "(Select c.chk_date,c.chk_ws,c.chk_barcode,(c.chk_qty-c.stk_qty) as adj_qty from stk_part_chk_di as c left join STK_Part_Chk_SI as d on c.chk_date=d.chk_date and c.chk_ws=d.chk_ws and c.chk_drumcode=d.chk_drumcode where d.chk_date=@m_chkdate and d.adjust_ok=0 ) as b ";
            m_update_sql += "on a.ws=b.chk_ws and a.barcode=b.chk_barcode where b.chk_barcode is not null ; ";
            m_update_sql += "update STK_Part_Chk_SI set adjust_ok=1 where chk_date=@m_chkdate and adjust_ok=0 ;";

            try
            {

                m_update_cmd.CommandText = m_update_sql;
                m_update_cmd.Parameters.Clear();

                m_update_cmd.Parameters.AddWithValue("m_userid", m_userid);
                m_update_cmd.Parameters.AddWithValue("m_chkdate", m_date);


                using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
                {
                    M_SQLConn.Open();
                    m_update_cmd.Connection = M_SQLConn;
                    m_update_cmd.ExecuteNonQuery();
                    M_SQLConn.Close();
                }
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "script", "alert('" + m_date + " 核帳調整完成')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel2, this.GetType(), "script", "alert('" + ex.Message + "')", true);
            }
            finally
            {
                m_update_cmd.Dispose();
                load_si_data();
            }

        }

    }
}