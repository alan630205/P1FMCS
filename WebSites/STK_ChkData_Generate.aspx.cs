using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class STK_ChkData_Generate : System.Web.UI.Page
{
    public string M_message = "訊息字串";
    public string M_connstr = ConfigurationManager.ConnectionStrings["P1FMCSConnectionString"].ConnectionString;
    public string M_CommText = "";

    private SqlConnection Conn = new SqlConnection();
    private SqlCommand M_QueryData_cmd = new SqlCommand();
    //private SqlDataAdapter M_da = new SqlDataAdapter();
    private SqlDataReader M_dr;


    protected void Page_Load(object sender, EventArgs e)
    {
        string m_script,m_url;
        m_url = "STK_Calendar.aspx?TextBoxId=" + TextBox1.ClientID;
        m_script = "window.open('" + m_url + "','','height=315,width=350,status=no,toolbar=no,menubar=no,location=no','')";
        if (!IsPostBack)
        {
            TextBox1.Text = DateTime.Now.ToString("yyyy/MM/dd");
            GET_DDL_DATA_WS();
            DropDownList1_SelectedIndexChanged(this, new EventArgs());
        }
        Button2.OnClientClick = m_script;
    }
    private void GET_DDL_DATA_WS()
    {
        string m_sqlstr = "SELECT WSNO FROM STK_WS Order By wsno ";

        using (SqlConnection DDL_strCon = new SqlConnection(M_connstr))
        {
            try
            {
                SqlCommand dropdownlist_cmd = new SqlCommand();
                dropdownlist_cmd.Connection = DDL_strCon;
                dropdownlist_cmd.CommandText = m_sqlstr;

                DDL_strCon.Open();

                SqlDataReader ddl = dropdownlist_cmd.ExecuteReader();

                DropDownList1.DataValueField = "wsno";
                DropDownList1.DataTextField = "wsno";

                DropDownList1.DataSource = ddl;
                DropDownList1.DataBind();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        string trn_tmp;
        string m_Strsql = "select top 1 * from STK_Chk_si where chk_ws=@m_ws and chk_ok=0 order by chk_date desc ";

        try
        {
            trn_tmp = DateTime.Parse(TextBox1.Text).ToString("yyyy/MM/dd");
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.ToString() + " (日期格式輸入錯誤)');</script>");
            return;
        }
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            try
            {
                //檢查是否有盤點未進行調整
                M_QueryData_cmd.Connection = M_SQLConn;
                M_SQLConn.Open();
                M_QueryData_cmd.CommandText = m_Strsql;
                M_QueryData_cmd.Parameters.Clear();
                M_QueryData_cmd.Parameters.AddWithValue("@m_ws", DropDownList1.SelectedValue.ToString());
                M_dr = M_QueryData_cmd.ExecuteReader();
                if (M_dr.HasRows)  //datareader有資料未調整
                {
                    M_dr.Read();
                    Response.Write("<script>alert('庫位：" + M_dr["chk_ws"].ToString().Trim() +"有盤點資料未進行調整！');</script>");
                }
                else
                {
                    //開始產生盤點資料
                    M_dr.Close();

                    SqlCommand cmd = new SqlCommand("STK_CHKDATA_GENERATE", M_SQLConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@m_ws",DropDownList1.SelectedValue.ToString() );
                    cmd.Parameters.AddWithValue("@m_chkdate",TextBox1.Text.Trim() );
                    cmd.Parameters.AddWithValue("@m_lst_chkdate",TextBox2.Text.Trim() );
                    cmd.ExecuteNonQuery();

                    Response.Write("<script>alert('庫位：" + DropDownList1.SelectedValue.ToString() + "盤點資料產生完成！');</script>");

                }

            }
            finally
            {
                M_dr.Close();
                M_dr.Dispose();
                M_SQLConn.Close();
                M_SQLConn.Dispose();
            }
        }
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string in_put = DropDownList1.SelectedValue.ToString();
        string m_Strsql = "select top 1 * from STK_Chk_si where chk_ws=@m_ws and chk_ok=1 order by chk_date desc " ;
        string m_lst_chkdate;

        M_QueryData_cmd.CommandText = m_Strsql;
        M_QueryData_cmd.Parameters.Clear();
        M_QueryData_cmd.Parameters.AddWithValue("@m_ws", DropDownList1.SelectedValue.ToString());
        using (SqlConnection M_SQLConn = new SqlConnection(M_connstr))
        {
            M_QueryData_cmd.Connection = M_SQLConn;
            M_SQLConn.Open();
            SqlDataReader M_dr = M_QueryData_cmd.ExecuteReader();

            if (M_dr.HasRows)  //datareader有資料
            {
                M_dr.Read();
                m_lst_chkdate = M_dr["chk_date"].ToString().Trim();
            }
            else
            {
                m_lst_chkdate = "";
            }
            M_dr.Close();
            M_dr.Dispose();
            M_SQLConn.Close();
            M_SQLConn.Dispose();
        }
        M_QueryData_cmd.Dispose();
        TextBox2.Text = m_lst_chkdate;
    }
}