using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace NTHPDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OracleConnection con;   // create connection

        List<String> forAppNo = new List<String>();
        List<String> forName = new List<String>();
        String conString = "User Id=sojharo;Password=sojharo;Data Source=orcl";//Sojharo";//orcl";//.15.2.148";

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int n;

            string data = "Following Box/es are not filled:\n\n";
            bool fill = true;
            bool yes = true;
            if (textBox1.Text == "" || textBox1.Text == null)
            {
                fill = false;
                data += "Application No.\n";
            }
            if (textBox3.Text == "" || textBox3.Text == null)
            {
                fill = false;
                data += "Name\n";
            }
            if (textBox4.Text == "" || textBox4.Text == null)
            {
                fill = false;
                data += "Father name\n";
            }
            if (!datePicker1.SelectedDate.HasValue)
            {
                fill = false;
                data += "Date Of Birth\n";
            }
            if (textBox29.Text == "" || textBox29.Text == null)
            {
                fill = false;
                data += "Degree Level\n";
            }
            if (textBox30.Text == "" || textBox30.Text == null)
            {
                fill = false;
                data += "Passing Year\n";
            }

            if (fill)
            {
                string invalidValues = "Invalid values: \n\n";
                if (!int.TryParse(textBox2.Text, out n))
                {
                    yes = false;
                    invalidValues += "Test Id:\t should be a number\n";
                }
                if (int.TryParse(textBox3.Text, out n))
                {
                    yes = false;
                    invalidValues += "Name:\t should not be a number\n";
                }
                if (int.TryParse(textBox4.Text, out n))
                {
                    yes = false;
                    invalidValues += "Father's Name:\t should not be a number\n";
                }
                if (int.TryParse(textBox5.Text, out n))
                {
                    yes = false;
                    invalidValues += "Test Center:\t should not be a number\n";
                }
                if (int.TryParse(textBox7.Text, out n))
                {
                    yes = false;
                    invalidValues += "Email Address:\t should not be a number\n";
                }
                if (int.TryParse(textBox18.Text, out n))
                {
                    yes = false;
                    invalidValues += "Domicile:\t should not be a number\n";
                }
                if (!int.TryParse(textBox30.Text, out n))
                {
                    yes = false;
                    invalidValues += "Passing Year:\t should be a number\n";
                }
                if (!int.TryParse(textBox32.Text, out n))
                {
                    yes = false;
                    invalidValues += "Marks Obtained:\t should be a number\n";
                }
                if (!int.TryParse(textBox33.Text, out n))
                {
                    yes = false;
                    invalidValues += "Total Marks:\t should be a number\n";
                }
                if (!int.TryParse(textBox34.Text, out n))
                {
                    yes = false;
                    invalidValues += "Percentage:\t should be a number\n";
                }
                if (!int.TryParse(textBox37.Text, out n))
                {
                    yes = false;
                    invalidValues += "Roll No:\t should be a number\n";
                }
                if (!int.TryParse(textBox20.Text, out n))
                {
                    yes = false;
                    invalidValues += "Father's Monthly Income:\t should be a number\n";
                }
                if (!int.TryParse(textBox21.Text, out n))
                {
                    yes = false;
                    invalidValues += "Others' Monthly Income:\t should be a number\n";
                }
                if (!int.TryParse(textBox22.Text, out n))
                {
                    yes = false;
                    invalidValues += "Annual Agricultural Income:\t should be a number\n";
                }
                if (!int.TryParse(textBox23.Text, out n))
                {
                    yes = false;
                    invalidValues += "Total Monthly Income:\t should be a number\n";
                }
                if (yes)
                    insertQuery();
                else
                    MessageBox.Show(invalidValues);
            }
            else
                MessageBox.Show("\n" + data);
                
        }

        public void fillUpdateList()
        {
            con = new OracleConnection(conString);

            OracleCommand com = new OracleCommand();
            con.Open();
            try
            {

                com.Connection = con;
                com.CommandText = "select app_no, name from Person_Info";
                com.CommandType = System.Data.CommandType.Text;
                OracleDataReader dr = com.ExecuteReader();

                dr.Read();

                forAppNo.Add("" + dr.GetValue(0));
                forName.Add("" + dr.GetValue(1));

                while (dr.Read())
                {
                    forAppNo.Add("" + dr.GetValue(0));
                    forName.Add("" + dr.GetValue(1));// + " - " + dr.GetValue(0));
                }

                foreach (String b in forName)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = b;

                    listBox1.Items.Add(item);
                }

            }
            catch (OracleException exception)
            {
                MessageBox.Show("Error: " + exception.Message);
            }
            finally
            {
                con.Dispose();
            }
        }

        public void fillInstitutes()
        {
            con = new OracleConnection(conString);

            OracleCommand com = new OracleCommand();
            con.Open();
            try
            {
                if (comboBox4.HasItems)
                    comboBox4.Items.Clear();

                com.Connection = con;
                com.CommandText = "select inst_no, inst_name from Institute";
                com.CommandType = System.Data.CommandType.Text;
                OracleDataReader dr = com.ExecuteReader();

                dr.Read();

                List<String> dataSource = new List<String>();
                dataSource.Add("" + dr.GetValue(0) + " " + dr.GetValue(1));

                while (dr.Read())
                {
                    dataSource.Add("" + dr.GetValue(0) + " " + dr.GetValue(1));
                }

                foreach (String b in dataSource)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = b;

                    comboBox4.Items.Add(item);
                }

            }
            catch (OracleException exception)
            {
                MessageBox.Show("Error: " + exception.Message);
            }
            finally
            {
                con.Dispose();
            }
        }

        public void insertQuery()
        {
            con = new OracleConnection(conString);

            OracleCommand com = new OracleCommand();
            OracleCommand com2 = new OracleCommand();
            try
            {
                //for personal_info table
                com.Connection = con;
                com.CommandText = "insert into Person_Info (app_no, name, f_name, dob, cnic," +
                    " test_id, email, domicile, restellno, mobile1, mobile2, test_center, f_income," +
                    " o_income, t_mon_income, an_agri_income, po_add, po_tehsil, po_district, po_prov," + 
                    " pe_add, pe_tehsil, pe_district, pe_prov) values"
                    + " (:app, :nam, :f, :d, :c, :t, :e, :dom, :rest, :mobil, :mobil2, :test," +
                    " :f_inc, :o_inc, :t_mon, :an_angri, :poa, :pot, :pod, :pop, :pea, :pet, :ped, :pep)";
                com.Parameters.Add(new OracleParameter(":app", textBox1.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":nam", textBox3.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":f", textBox4.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":d", datePicker1.SelectedDate.Value));
                com.Parameters.Add(new OracleParameter(":c", textBox19.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":t", Int32.Parse(textBox2.Text.Trim())));
                com.Parameters.Add(new OracleParameter(":e", textBox7.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":dom", textBox18.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":rest", textBox6.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":mobil", textBox8.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":mobil2", textBox9.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":test", textBox5.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":f_inc", Int32.Parse(textBox20.Text.Trim())));
                com.Parameters.Add(new OracleParameter(":o_inc", Int32.Parse(textBox21.Text.Trim())));
                com.Parameters.Add(new OracleParameter(":t_mon", Int32.Parse(textBox23.Text.Trim())));
                com.Parameters.Add(new OracleParameter(":an_agri", Int32.Parse(textBox22.Text.Trim())));
                com.Parameters.Add(new OracleParameter(":poa", textBox14.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":pot", textBox15.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":pod", textBox16.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":pop", textBox17.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":pea", textBox10.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":pet", textBox11.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":ped", textBox12.Text.Trim()));
                com.Parameters.Add(new OracleParameter(":pep", textBox13.Text.Trim()));

                con.Open();
                com.ExecuteNonQuery();
                //end personal_info table

                forName.Add(textBox3.Text.Trim());
                forAppNo.Add(textBox1.Text.Trim());
                updateUpdateList();

                int newKeyNo=-1;

                //for institute table
                if (comboBox4.SelectedIndex == -1)
                {
                    OracleCommand comTemp = new OracleCommand();
                    comTemp.Connection = con;
                    comTemp.CommandText = "select inst_no from Institute";
                    comTemp.CommandType = System.Data.CommandType.Text;
                    OracleDataReader dr = comTemp.ExecuteReader();

                    dr.Read();

                    try
                    {
                        newKeyNo = Int32.Parse(dr.GetValue(0).ToString()); ;
                    }
                    catch (System.InvalidOperationException e)
                    {
                        Console.WriteLine(e.Message);
                        newKeyNo = 0;
                    }

                    while (dr.Read())
                    {
                        newKeyNo = Int32.Parse(dr.GetValue(0).ToString());
                    }

                    newKeyNo += 1;
                    //MessageBox.Show("" + newKeyNo);
                    com2.Connection = con;
                    com2.CommandText = "insert into Institute (inst_no, inst_name, board, i_tehsil," +
                        " i_district, i_province) values (:i_no, :i_nam, :b, :i_t, :i_d, :i_p)";
                    com2.Parameters.Add(new OracleParameter(":i_no", newKeyNo));
                    com2.Parameters.Add(new OracleParameter(":i_nam", textBox24.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":b", textBox25.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":i_t", textBox26.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":i_d", textBox27.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":i_p", textBox28.Text.Trim()));

                    com2.ExecuteNonQuery();

                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = "" + newKeyNo + " " + textBox24.Text.Trim();

                    comboBox4.Items.Add(item);
                    //End Institute Table
                }
                else
                {
                    ComboBoxItem typeItem = (ComboBoxItem)comboBox4.SelectedItem;
                    string value = typeItem.Content.ToString();
                    newKeyNo = Int32.Parse(value.Substring(0, value.IndexOf(" ")).Trim());
                }

                //for Academic_Info Table with Instite new
                OracleCommand comTemp2 = new OracleCommand();
                comTemp2.Connection = con;

                comTemp2.CommandText = "insert into Academic ( deglevel, roll_no, pyear, stream_group, t_marks," +
               " m_obtained, st_percentage, st_grade, app_no, inst_no, st_position ) values (:deg, :rol, :pye, :st, :tm," +
               " :mo, :pr, :gra, :ap, :i, :p)";

                comTemp2.Parameters.Add(new OracleParameter(":deg", textBox29.Text.Trim()));
                comTemp2.Parameters.Add(new OracleParameter(":rol", int.Parse(textBox37.Text.Trim())));
                comTemp2.Parameters.Add(new OracleParameter(":pye", int.Parse(textBox30.Text.Trim())));
                comTemp2.Parameters.Add(new OracleParameter(":st", textBox31.Text.Trim()));
                comTemp2.Parameters.Add(new OracleParameter(":tm", int.Parse(textBox33.Text.Trim())));
                comTemp2.Parameters.Add(new OracleParameter(":mo", int.Parse(textBox32.Text.Trim())));
                comTemp2.Parameters.Add(new OracleParameter(":pr", int.Parse(textBox34.Text.Trim())));
                comTemp2.Parameters.Add(new OracleParameter(":gra", textBox35.Text.Trim()));
                comTemp2.Parameters.Add(new OracleParameter(":ap", textBox1.Text.Trim()));
                comTemp2.Parameters.Add(new OracleParameter(":i", newKeyNo));
                comTemp2.Parameters.Add(new OracleParameter(":p", textBox36.Text.Trim()));

                comTemp2.ExecuteNonQuery();
                //End Academic_Info Table with Institute new

                MessageBox.Show("Inserted Successfully!");
            }
            catch (OracleException exception)
            {
                MessageBox.Show("Error: " + exception.Message + " " + exception.Source + "\n\n " + exception.StackTrace);
            }
            finally
            {
                con.Close();
            }
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button13)
            {
                updateUpdateList();
            }
        }

        public void updateUpdateList()
        {
            try
            {
                listBox1.Items.Clear();
            }
            catch (System.Reflection.TargetInvocationException ed)
            {
                Console.WriteLine(ed.Message);
            }
            finally { }

            if (comboBox3.SelectedIndex == 0)
            {
                foreach (String b in forName)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = b;

                    listBox1.Items.Add(item);
                }
            }
            else
            {
                foreach (String b in forAppNo)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = b;

                    listBox1.Items.Add(item);
                }
            }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source == listBox1 && listBox1.HasItems)
            {
                string b = forAppNo.ElementAt(listBox1.SelectedIndex);

                con = new OracleConnection(conString);
                
                OracleCommand com = new OracleCommand();
                con.Open();
                try
                {
                    
                   com.Connection = con;
                   com.CommandText = "select * from Person_Info, Academic, Institute "+
                       "where Person_Info.app_no = Academic.app_no and Institute.inst_no = "+
                       "Academic.inst_no and Person_Info.app_no = :appno";

                   com.Parameters.Add(new OracleParameter(":appno", "" + b));

                   com.CommandType = System.Data.CommandType.Text;
                   OracleDataReader dr = com.ExecuteReader();

                   while(dr.Read())
                   {
                       textBox39.Text = "" + dr.GetValue(1).ToString().Trim();
                       textBox40.Text = "" + dr.GetValue(2).ToString().Trim();
                       textBox41.Text = "" + dr.GetValue(7).ToString().Trim();
                      // textBox42.Text = "" + dr.GetValue(3).ToString().Trim();
                       textBox43.Text = "" + dr.GetValue(4).ToString().Trim();
                       textBox44.Text = "" + dr.GetValue(5).ToString().Trim();
                       textBox45.Text = "" + dr.GetValue(6).ToString().Trim();
                       textBox46.Text = "" + dr.GetValue(8).ToString().Trim();
                       textBox47.Text = "" + dr.GetValue(9).ToString().Trim();
                       textBox48.Text = "" + dr.GetValue(10).ToString().Trim();
                       textBox49.Text = "" + dr.GetValue(23).ToString().Trim();
                       textBox50.Text = "" + dr.GetValue(24).ToString().Trim();
                       textBox51.Text = "" + dr.GetValue(25).ToString().Trim();
                       textBox52.Text = "" + dr.GetValue(26).ToString().Trim();
                       textBox53.Text = "" + dr.GetValue(19).ToString().Trim();
                       textBox54.Text = "" + dr.GetValue(20).ToString().Trim();
                       textBox55.Text = "" + dr.GetValue(21).ToString().Trim();
                       textBox56.Text = "" + dr.GetValue(22).ToString().Trim();
                       textBox57.Text = "" + dr.GetValue(27).ToString().Trim();
                       textBox58.Text = "" + dr.GetValue(29).ToString().Trim();
                       textBox59.Text = "" + dr.GetValue(30).ToString().Trim();
                       textBox60.Text = "" + dr.GetValue(32).ToString().Trim();
                       textBox61.Text = "" + dr.GetValue(31).ToString().Trim();
                       textBox62.Text = "" + dr.GetValue(33).ToString().Trim();
                       textBox63.Text = "" + dr.GetValue(34).ToString().Trim();
                       textBox64.Text = "" + dr.GetValue(37).ToString().Trim();
                       textBox65.Text = "" + dr.GetValue(28).ToString().Trim();
                       textBox66.Text = "" + dr.GetValue(39).ToString().Trim();
                       textBox67.Text = "" + dr.GetValue(40).ToString().Trim();
                       textBox68.Text = "" + dr.GetValue(41).ToString().Trim();
                       textBox69.Text = "" + dr.GetValue(42).ToString().Trim();
                       textBox70.Text = "" + dr.GetValue(43).ToString().Trim();
                       textBox71.Text = "" + dr.GetValue(11).ToString().Trim();
                       textBox72.Text = "" + dr.GetValue(12).ToString().Trim();
                       textBox73.Text = "" + dr.GetValue(13).ToString().Trim();
                       textBox75.Text = "" + dr.GetValue(14).ToString().Trim();
                       textBox76.Text = "" + dr.GetValue(18).ToString().Trim();
                       textBox77.Text = "" + dr.GetValue(15).ToString().Trim();
                       textBox78.Text = "" + dr.GetValue(16).ToString().Trim();
                       textBox79.Text = "" + dr.GetValue(17).ToString().Trim();
                   }
                }
                catch (OracleException exception)
                {
                    MessageBox.Show("Error: " + exception.Message +" "+ exception.StackTrace);
                }
                finally
                {
                    con.Dispose();
                }
            }
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button14 && listBox1.SelectedIndex != -1)
            {
                con = new OracleConnection(conString);
                //OracleCommand
                OracleCommand com = new OracleCommand();
                OracleCommand com2 = new OracleCommand();
                OracleCommand com3 = new OracleCommand();
                OracleCommand comTemp = new OracleCommand();
                con.Open();
                try
                {

                    com.Connection = con;
                    com.CommandText = "update Person_Info set " +
                        "name = :nm, " +
                        "f_name = :f_nm, " +                        
                        "cnic = :nic, " +
                        "test_id = :tst_id, " +
                        "email = :mail, " +
                        "domicile = :dom, " +
                        "restellno = :rstellno, " +
                        "mobile1 = :mob1, " +
                        "mobile2 = :mob2, " +
                        "f_income = :f_inc, " +
                        "o_income = :o_inc, " +
                        "t_mon_income = :t_mon_inc, " +
                        "an_agri_income = :an_agri_inc, " +
                        "gotin_prog = :gtin_prog, " +
                        "gotin_iba = :gtin_iba, " +
                        "admittedin_iba = :admttedin_iba, " +
                        "test_center = :test_cnter, " +
                        "po_add = :po_ad, " +
                        "po_tehsil = :po_tehsl, " +
                        "po_district = :po_distrct, " +
                        "po_prov = :po_prv, " +
                        "pe_add = :pe_ad, " +
                        "pe_tehsil = :pe_tehsl, " +
                        "pe_district = :pe_distrct, " +
                        "pe_prov = :pe_prv " +
                        "where app_no = :selectedKey";

                    //com.Parameters.Add(new OracleParameter(":selectedKe", forAppNo.ElementAt(listBox1.SelectedIndex)));
                    com.Parameters.Add(new OracleParameter(":nm", textBox39.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":f_nm", textBox40.Text.Trim()));
                    //com.Parameters.Add(new OracleParameter(":db", textBox42.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":nic", textBox43.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":tst_id", textBox44.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":mail", textBox45.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":dom", textBox41.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":rstellno", textBox46.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":mob1", textBox47.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":mob2", textBox48.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":f_inc", textBox71.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":o_inc", textBox72.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":t_mon_inc", textBox73.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":an_agri_inc", textBox75.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":gtin_prog", textBox77.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":gtin_iba", textBox78.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":admttedin_iba", textBox79.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":test_cnter", textBox76.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":po_ad", textBox53.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":po_tehsl", textBox54.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":po_distrct", textBox55.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":po_prv", textBox56.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":pe_ad", textBox49.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":pe_tehsl", textBox50.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":pe_distrct", textBox51.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":pe_prv", textBox52.Text.Trim()));
                    com.Parameters.Add(new OracleParameter(":selectedKey", forAppNo.ElementAt(listBox1.SelectedIndex)));

                    com.ExecuteNonQuery();

                    ///////////////////////////////////////////////////////////////

                    comTemp.Connection = con;
                    comTemp.CommandText = "select Institute.inst_no, Person_Info.app_no, st_grade from institute, " +
                         "person_info, academic where institute.inst_no = academic.inst_no " +
                         "and person_info.app_no = academic.app_no and Person_Info.app_no = " +
                         ":selectedKey";
                    comTemp.Parameters.Add(new OracleParameter(":selectedKey", "" + forAppNo.ElementAt(listBox1.SelectedIndex)));
                    comTemp.CommandType = System.Data.CommandType.Text;

                    OracleDataReader dr = comTemp.ExecuteReader();

                    String appnoTemp = "";
                    String instnoTemp = "";

                    while (dr.Read())
                    {
                        appnoTemp = "" + dr.GetValue(1).ToString().Trim();
                        instnoTemp = "" + dr.GetValue(0).ToString().Trim();
                    }

                    ///////////////////////////////////////////////////////////////

                    com2.Connection = con;
                    com2.CommandText = "update Institute set " +
                        "inst_name = :nm, " +
                        "board = :b_nm, " +
                        "i_tehsil = :i_tehsl, " +
                        "i_district = :i_distrct, " +
                        "i_province = :i_prv " +
                        "where inst_no = :inst";

                    com2.Parameters.Add(new OracleParameter(":nm", textBox66.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":b_nm", textBox67.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":i_tehsl", textBox68.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":i_distrct", textBox69.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":i_prv", textBox70.Text.Trim()));
                    com2.Parameters.Add(new OracleParameter(":inst", instnoTemp));

                    com2.ExecuteNonQuery();

                    com3.Connection = con;
                    com3.CommandText = "update Academic set " +
                        "deglevel = :dgi, " +
                        "roll_no = :rni, " +
                        "pyear = :pyi, " +
                        "stream_group = :sti, " +
                        "t_marks = :tmi, " +
                        "m_obtained = :omi, " +
                        "st_percentage = :pri, " +
                        "st_grade = :gdi, " +
                        "st_position = :psi " +
                        "where (inst_no = :inst and app_no = :app)";

                    com3.Parameters.Add(new OracleParameter(":dgi", textBox57.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":rni", textBox65.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":pyi", textBox58.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":sti", textBox59.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":tmi", textBox61.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":omi", textBox60.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":pri", textBox62.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":gdi", textBox63.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":psi", textBox64.Text.Trim()));
                    com3.Parameters.Add(new OracleParameter(":inst", instnoTemp));
                    com3.Parameters.Add(new OracleParameter(":app", appnoTemp));

                    com3.ExecuteNonQuery();

                    MessageBox.Show("Query Updated.");

                }
                catch (OracleException exception)
                {
                    MessageBox.Show("Error: " + exception.Message + " " + exception.StackTrace);
                }
                finally
                {
                    con.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Select any record to update.");
            }
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button15)
            {
                var erDiagram = new Window1();
                erDiagram.Show();
            }
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            
            if (e.Source == button12)
            {
                if (textBox74.Text.ToString().Equals(""))
                {
                    MessageBox.Show("Write the query to execute.");
                    return;
                }
                if (textBox74.Text.Trim().IndexOf(';') == -1)
                {
                    try
                    {
                        using (OracleConnection con = new OracleConnection(conString))
                        {
                            OracleCommand cmd = new OracleCommand();
                            cmd.Connection = con;
                            cmd.CommandText = textBox74.Text.Trim();
                            OracleDataAdapter da = new OracleDataAdapter(cmd);
                            System.Data.DataTable dt = new System.Data.DataTable();
                            da.Fill(dt);
                            dataGrid1.ItemsSource = dt.DefaultView;
                        }
                    }
                    catch (OracleException e2)
                    {
                        MessageBox.Show("Error: " + e2.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Kindly remove the the symbols like ; from your query.");
                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button4)
            {
                textBox1.Text = null;
                textBox2.Text = null; 
                textBox3.Text = null;
                textBox4.Text = null;
                textBox5.Text = null;
                textBox6.Text = null;
                textBox7.Text = null;
                textBox8.Text = null;
                textBox9.Text = null;
                textBox10.Text = null;
                textBox11.Text = null;
                textBox12.Text = null;
                textBox13.Text = null;
                textBox14.Text = null;
                textBox15.Text = null;
                textBox16.Text = null;
                textBox17.Text = null;
                textBox18.Text = null;
                textBox19.Text = null;
                textBox20.Text = null;
                textBox21.Text = null;
                textBox22.Text = null;
                textBox23.Text = null;
                // datePicker1.SelectedDate.Value = null;
                // sojharo you have to make date
                // null.  Achho thai muh tuhnjo
                // pagal adda HE KAJA 
                // WIR JA NA AADDAAAAA
                textBox24.Text = null;
                textBox25.Text = null;
                textBox26.Text = null;
                textBox27.Text = null;
                textBox28.Text = null;
                textBox29.Text = null;
                textBox30.Text = null;
                textBox31.Text = null;
                textBox32.Text = null;
                textBox33.Text = null;
                textBox34.Text = null;
                textBox35.Text = null;
                textBox36.Text = null;
                textBox37.Text = null;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
            textBox9.Text = null;
            textBox10.Text = null;
            textBox11.Text = null;
            textBox12.Text = null;
            textBox13.Text = null;
            textBox14.Text = null;
            textBox15.Text = null;
            textBox16.Text = null;
            textBox17.Text = null;
            textBox18.Text = null;
            textBox19.Text = null;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button2)
            {
                tabControl2.SelectedIndex = 1;
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button7)
                tabControl2.SelectedIndex = 2;
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button8)
                tabControl2.SelectedIndex = 0;
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button6)
                tabControl2.SelectedIndex = 1;
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            textBox24.Text = null;
            textBox25.Text = null;
            textBox26.Text = null;
            textBox27.Text = null;
            textBox28.Text = null;
            textBox29.Text = null;
            textBox30.Text = null;
            textBox31.Text = null;
            textBox32.Text = null;
            textBox33.Text = null;
            textBox34.Text = null;
            textBox35.Text = null;
            textBox36.Text = null;
            textBox37.Text = null;
            comboBox4.SelectedIndex = -1;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            textBox20.Text = null;
            textBox21.Text = null;
            textBox22.Text = null;
            textBox23.Text = null;
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            if (textBox81.Text.Trim().Equals("") || textBox81.Text.Trim() == null)
            {
                MessageBox.Show("Kindly fill the first row.");
                return ;
            }

            string basicConString = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no ";

            string value2 = "";

            if (comboBox9.SelectedIndex == 0)
                value2 = ">";
            else if (comboBox9.SelectedIndex == 1)
                value2 = "<";
            else if (comboBox9.SelectedIndex == 2)
                value2 = "=";
            else if (comboBox9.SelectedIndex == 3)
                value2 = "<>";
            else if (comboBox9.SelectedIndex == 4)
                value2 = "like";

            string value3 = "";

            if (comboBox10.SelectedIndex == 0)
                value3 = ">";
            else if (comboBox10.SelectedIndex == 1)
                value3 = "<";
            else if (comboBox10.SelectedIndex == 2)
                value3 = "=";
            else if (comboBox10.SelectedIndex == 3)
                value3 = "<>";
            else if (comboBox10.SelectedIndex == 4)
                value3 = "like";

            string value4 = "";

            if (comboBox11.SelectedIndex == 0)
                value4 = ">";
            else if (comboBox11.SelectedIndex == 1)
                value4 = "<";
            else if (comboBox11.SelectedIndex == 2)
                value4 = "=";
            else if (comboBox11.SelectedIndex == 3)
                value4 = "<>";
            else if (comboBox11.SelectedIndex == 4)
                value4 = "like";

            string value5 = "";

            if (comboBox12.SelectedIndex == 0)
                value5 = ">";
            else if (comboBox12.SelectedIndex == 1)
                value5 = "<";
            else if (comboBox12.SelectedIndex == 2)
                value5 = "=";
            else if (comboBox12.SelectedIndex == 3)
                value5 = "<>";
            else if (comboBox12.SelectedIndex == 4)
                value5 = "like";

            string value6 = "";

            if (comboBox17.SelectedIndex == 0)
                value6 = ">";
            else if (comboBox17.SelectedIndex == 1)
                value6 = "<";
            else if (comboBox17.SelectedIndex == 2)
                value6 = "=";
            else if (comboBox17.SelectedIndex == 3)
                value6 = "<>";
            else if (comboBox17.SelectedIndex == 4)
                value6 = "like";

            if (e.Source == button11)
            {
                try
                {
                    using (OracleConnection connn = new OracleConnection(conString))
                    {
                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = connn;

                        #region first_row
                        if (comboBox1.SelectedIndex == 0)
                        {
                            cmd.CommandText = basicConString +
                                "and person_info.po_district " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 1)
                        {
                            cmd.CommandText = basicConString +
                                "and academic.st_grade " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 2)
                        {
                            cmd.CommandText = basicConString +
                                "and person_info.domicile " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 3)
                        {
                            cmd.CommandText = basicConString +
                                "and institute.board " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 4)
                        {
                            cmd.CommandText = basicConString +
                                "and academic.pyear " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox81.Text.Trim())));
                        }
                        else if (comboBox1.SelectedIndex == 5)
                        {
                            cmd.CommandText = basicConString +
                                "and academic.deglevel " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 6)
                        {
                            cmd.CommandText = basicConString +
                                "and person_info.test_id " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox81.Text.Trim())));
                        }
                        else if (comboBox1.SelectedIndex == 7)
                        {
                            cmd.CommandText = basicConString +
                                "and person_info.f_name " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 8)
                        {
                            cmd.CommandText = basicConString +
                                "and person_info.name " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 9)
                        {
                            cmd.CommandText = basicConString +
                                "and person_info.dob " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 10)
                        {
                            cmd.CommandText = basicConString +
                                "and person_info.cnic " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 11)
                        {
                            cmd.CommandText = basicConString +
                                "and academic.m_obtained " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox81.Text.Trim())));
                        }
                        else if (comboBox1.SelectedIndex == 12)
                        {
                            cmd.CommandText = basicConString +
                                "and academic.st_percentage " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox81.Text.Trim())));
                        }
                        else if (comboBox1.SelectedIndex == 13)
                        {
                            cmd.CommandText = basicConString +
                                "and academic.st_position " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 14)
                        {
                            cmd.CommandText = basicConString +
                                "and institute.i_tehsil " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 15)
                        {
                            cmd.CommandText = basicConString +
                                "and institute.i_district " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 16)
                        {
                            cmd.CommandText = basicConString +
                                "and institute.i_prov " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 17)
                        {
                            cmd.CommandText = basicConString +
                                "and academic.stream_group " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        else if (comboBox1.SelectedIndex == 18)
                        {
                            cmd.CommandText = basicConString +
                                "and academic.roll_no " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox81.Text.Trim())));
                        }
                        else if (comboBox1.SelectedIndex == 19)
                        {
                            cmd.CommandText = basicConString +
                                "and institute.inst_name " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox81.Text.Trim()));
                        }
                        #endregion first_row

                        #region second_row
                        if (!textBox82.Text.Equals(""))
                        {
                            if (comboBox2.SelectedIndex == 0)
                            {
                                cmd.CommandText += " "+ comboBox13.Text +
                                    " person_info.po_district " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 1)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " academic.st_grade " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 2)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " person_info.domicile " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 3)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " institute.board " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 4)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " academic.pyear " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", int.Parse(textBox82.Text.Trim())));
                            }
                            else if (comboBox2.SelectedIndex == 5)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " academic.deglevel " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 6)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " person_info.test_id " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", int.Parse(textBox82.Text.Trim())));
                            }
                            else if (comboBox2.SelectedIndex == 7)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " person_info.f_name " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 8)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " person_info.name " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 9)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " person_info.dob " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 10)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " person_info.cnic " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 11)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " academic.m_obtained " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", int.Parse(textBox82.Text.Trim())));
                            }
                            else if (comboBox2.SelectedIndex == 12)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " academic.st_percentage " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", int.Parse(textBox82.Text.Trim())));
                            }
                            else if (comboBox2.SelectedIndex == 13)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " academic.st_position " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 14)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " institute.i_tehsil " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 15)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " institute.i_district " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 16)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " institute.i_prov " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 17)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " academic.stream_group " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                            else if (comboBox2.SelectedIndex == 18)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " academic.roll_no " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", int.Parse(textBox82.Text.Trim())));
                            }
                            else if (comboBox2.SelectedIndex == 19)
                            {
                                cmd.CommandText += " " + comboBox13.Text +
                                    " institute.inst_name " + value3 + " :variab3";
                                cmd.Parameters.Add(new OracleParameter(":variab3", textBox82.Text.Trim()));
                            }
                        }
                        #endregion second_row

                        #region third_row
                        if (!textBox83.Text.Equals(""))
                        {
                            if (comboBox7.SelectedIndex == 0)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " person_info.po_district " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 1)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " academic.st_grade " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 2)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " person_info.domicile " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 3)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " institute.board " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 4)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " academic.pyear " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", int.Parse(textBox83.Text.Trim())));
                            }
                            else if (comboBox7.SelectedIndex == 5)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " academic.deglevel " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 6)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " person_info.test_id " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", int.Parse(textBox83.Text.Trim())));
                            }
                            else if (comboBox7.SelectedIndex == 7)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " person_info.f_name " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 8)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " person_info.name " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 9)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " person_info.dob " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 10)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " person_info.cnic " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 11)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " academic.m_obtained " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", int.Parse(textBox83.Text.Trim())));
                            }
                            else if (comboBox7.SelectedIndex == 12)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " academic.st_percentage " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", int.Parse(textBox83.Text.Trim())));
                            }
                            else if (comboBox7.SelectedIndex == 13)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " academic.st_position " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 14)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " institute.i_tehsil " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 15)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " institute.i_district " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 16)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " institute.i_prov " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 17)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " academic.stream_group " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                            else if (comboBox7.SelectedIndex == 18)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " academic.roll_no " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", int.Parse(textBox83.Text.Trim())));
                            }
                            else if (comboBox7.SelectedIndex == 19)
                            {
                                cmd.CommandText += " " + comboBox14.Text +
                                    " institute.inst_name " + value4 + " :variab4";
                                cmd.Parameters.Add(new OracleParameter(":variab4", textBox83.Text.Trim()));
                            }
                        }
                        #endregion third_row

                        #region forth_row
                        if (!textBox84.Text.Equals(""))
                        {
                            if (comboBox8.SelectedIndex == 0)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " person_info.po_district " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 1)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " academic.st_grade " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 2)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " person_info.domicile " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 3)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " institute.board " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 4)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " academic.pyear " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", int.Parse(textBox84.Text.Trim())));
                            }
                            else if (comboBox8.SelectedIndex == 5)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " academic.deglevel " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 6)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " person_info.test_id " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", int.Parse(textBox84.Text.Trim())));
                            }
                            else if (comboBox8.SelectedIndex == 7)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " person_info.f_name " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 8)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " person_info.name " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 9)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " person_info.dob " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 10)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " person_info.cnic " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 11)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " academic.m_obtained " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", int.Parse(textBox84.Text.Trim())));
                            }
                            else if (comboBox8.SelectedIndex == 12)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " academic.st_percentage " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", int.Parse(textBox84.Text.Trim())));
                            }
                            else if (comboBox8.SelectedIndex == 13)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " academic.st_position " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 14)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " institute.i_tehsil " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 15)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " institute.i_district " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 16)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " institute.i_prov " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 17)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " academic.stream_group " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                            else if (comboBox8.SelectedIndex == 18)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " academic.roll_no " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", int.Parse(textBox84.Text.Trim())));
                            }
                            else if (comboBox8.SelectedIndex == 19)
                            {
                                cmd.CommandText += " " + comboBox15.Text +
                                    " institute.inst_name " + value5 + " :variab5";
                                cmd.Parameters.Add(new OracleParameter(":variab5", textBox84.Text.Trim()));
                            }
                        }
                        #endregion forth_row

                        #region fifth_row
                        if (!textBox85.Text.Equals(""))
                        {
                            if (comboBox16.SelectedIndex == 0)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " person_info.po_district " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 1)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " academic.st_grade " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 2)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " person_info.domicile " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 3)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " institute.board " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 4)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " academic.pyear " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", int.Parse(textBox85.Text.Trim())));
                            }
                            else if (comboBox16.SelectedIndex == 5)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " academic.deglevel " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 6)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " person_info.test_id " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", int.Parse(textBox85.Text.Trim())));
                            }
                            else if (comboBox16.SelectedIndex == 7)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " person_info.f_name " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 8)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " person_info.name " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 9)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " person_info.dob " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 10)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " person_info.cnic " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 11)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " academic.m_obtained " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", int.Parse(textBox85.Text.Trim())));
                            }
                            else if (comboBox16.SelectedIndex == 12)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " academic.st_percentage " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", int.Parse(textBox85.Text.Trim())));
                            }
                            else if (comboBox16.SelectedIndex == 13)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " academic.st_position " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 14)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " institute.i_tehsil " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 15)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " institute.i_district " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 16)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " institute.i_prov " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 17)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " academic.stream_group " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                            else if (comboBox16.SelectedIndex == 18)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " academic.roll_no " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", int.Parse(textBox85.Text.Trim())));
                            }
                            else if (comboBox16.SelectedIndex == 19)
                            {
                                cmd.CommandText += " " + comboBox18.Text +
                                    " institute.inst_name " + value6 + " :variab6";
                                cmd.Parameters.Add(new OracleParameter(":variab6", textBox85.Text.Trim()));
                            }
                        }
                        #endregion fifth_row

                        Window2 windowGrid = new Window2();

                        //MessageBox.Show(cmd.CommandText.ToString());
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        da.Fill(dt);
                        //dataGrid2.ItemsSource = dt.DefaultView;
                        windowGrid.Show();
                        windowGrid.dataGrid1.ItemsSource = dt.DefaultView;
                        
                    }
                }
                catch (OracleException e2)
                {
                    MessageBox.Show("Error: " + e2.Message);
                }
            }

        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            string value2 = "";

            if (comboBox6.SelectedIndex == 0)
                value2 = ">";
            else if (comboBox6.SelectedIndex == 1)
                value2 = "<";
            else if (comboBox6.SelectedIndex == 2)
                value2 = "=";
            else if (comboBox6.SelectedIndex == 3)
                value2 = "<>";
            else if (comboBox6.SelectedIndex == 4)
                value2 = "like";

            if (e.Source == button10)
            {
                try
                {
                    using (OracleConnection con = new OracleConnection(conString))
                    {
                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = con;

                        if (comboBox5.SelectedIndex == 0)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and person_info.po_district " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 1)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and academic.st_grade " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 2)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and person_info.domicile " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 3)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and institute.board " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 4)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and academic.pyear " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox80.Text.Trim())));
                        }
                        else if (comboBox5.SelectedIndex == 5)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and academic.deglevel " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 6)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and person_info.test_id " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox80.Text.Trim())));
                        }
                        else if (comboBox5.SelectedIndex == 7)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and person_info.f_name " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 8)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and person_info.name " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 9)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and person_info.dob " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 10)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and person_info.cnic " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 11)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and academic.m_obtained " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox80.Text.Trim())));
                        }
                        else if (comboBox5.SelectedIndex == 12)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and academic.st_percentage " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox80.Text.Trim())));
                        }
                        else if (comboBox5.SelectedIndex == 13)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and academic.st_position " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 14)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and institute.i_tehsil " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 15)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and institute.i_district " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 16)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and institute.i_prov " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 17)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and academic.stream_group " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }
                        else if (comboBox5.SelectedIndex == 18)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and academic.roll_no " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", int.Parse(textBox80.Text.Trim())));
                        }
                        else if (comboBox5.SelectedIndex == 19)
                        {
                            cmd.CommandText = "select * from person_info, " +
                                "academic, institute where institute.inst_no = academic.inst_no " +
                                "and person_info.app_no = academic.app_no " +
                                "and institute.inst_name " + value2 + " :variab2";
                            cmd.Parameters.Add(new OracleParameter(":variab2", textBox80.Text.Trim()));
                        }

                        //MessageBox.Show(cmd.CommandText.ToString());
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        da.Fill(dt);
                        dataGrid2.ItemsSource = dt.DefaultView;
                    }
                }
                catch (OracleException e2)
                {
                    MessageBox.Show("Error: " + e2.Message);
                }
            }
        }

        private void button17_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                fillInstitutes();
                fillUpdateList();
            }
            finally
            {
                MessageBox.Show("Connected to the database.");
            }
        }

        private void button16_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == button16)
            {
                textBox14.Text = textBox10.Text;
                textBox15.Text = textBox11.Text;
                textBox16.Text = textBox12.Text;
                textBox17.Text = textBox13.Text;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@"Manual.pdf");
            }
            catch (System.ComponentModel.Win32Exception exx)
            {
                MessageBox.Show("Help file could not be loaded. Someone might have removed it.");
            }
        }


    }
}
