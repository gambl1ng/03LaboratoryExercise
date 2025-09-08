using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03LaboratoryExercise
{
    public partial class frmRegistration : Form
    {
        private string _FullName;
        private int _Age;
        private long _ContactNo;
        private long _StudentNo;

        public frmRegistration()
        {
            InitializeComponent();
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[] {
                 "BS Information Technology",
                 "BS Computer Science",
                 "BS Information Systems",
                 "BS in Accountancy",
                 "BS in Hospitality Management",
                 "BS in Tourism Management"
            };
            for (int i = 0; i < 6; i++)
            {
                cbPrograms.Items.Add(ListOfProgram[i].ToString());
            }
                cbGender.Items.Add("Male");
                cbGender.Items.Add("Female");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                StudentInformationClass.SetFullName = FullName(txtLastName.Text, txtFirstName.Text, txtMiddleInitial.Text);
                StudentInformationClass.SetStudentNo = StudentNumber(txtStudentNo.Text);
                StudentInformationClass.SetProgram = cbPrograms.Text;
                StudentInformationClass.SetGender = cbGender.Text;
                StudentInformationClass.SetContactNo = ContactNo(txtContactNo.Text);
                StudentInformationClass.SetAge = Age(txtAge.Text);
                StudentInformationClass.SetBirthday = datePickerBirthday.Value.ToString("yyyy-MM-dd");

                frmConfirmation frm = new frmConfirmation();
                frm.ShowDialog();
            }
            catch (FormatException f)
            {
                MessageBox.Show(f.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentNullException a)
            {
                MessageBox.Show(a.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException o)
            {
                MessageBox.Show(o.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IndexOutOfRangeException i)
            {
                MessageBox.Show(i.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtStudentNo.Text = "";
                txtLastName.Text = "";
                txtFirstName.Text = "";
                txtMiddleInitial.Text = "";
                txtContactNo.Text = "";
                txtAge.Text = "";
            }
        }
public long StudentNumber(string studNum)
        {
            long tempStudentNo;
            if (long.TryParse(studNum, out tempStudentNo))
            {
                _StudentNo = tempStudentNo;
            }
            else
            {
                throw new FormatException("Invalid student number. Ensure you only use digits and it has the correct length.");
            }
            return _StudentNo;
        }
        public long ContactNo(string Contact)
        {
            if (Regex.IsMatch(Contact, @"^[0-9]{10,11}$"))
            {
                _ContactNo = long.Parse(Contact);
            }
            else
            {
                throw new FormatException("The contact number format is incorrect. It must be 10 or 11 digits long only.");
            }
            return _ContactNo;
        }
        public string FullName(string LastName, string FirstName, string MiddleInitial)
        {
            if (Regex.IsMatch(LastName, @"^[a-zA-Z]+$") && Regex.IsMatch(FirstName, @"^[a-zA-Z]+$") && Regex.IsMatch(MiddleInitial, @"^[a-zA-Z]+$"))
            {
                _FullName = LastName + ", " + FirstName + ", " + MiddleInitial;
            }
            else
            {
                throw new FormatException("Invalid characters in the name. Only letters are permitted.");
            }
            return _FullName;
        }
        public int Age(string age)
        {
            int tempAge;
            if (int.TryParse(age, out tempAge))
            {
                if (tempAge < 0 || tempAge > 100)
                {
                    throw new OverflowException("The age is outside of a valid range.");
                }
                _Age = tempAge;
            }
            else
            {
                throw new FormatException("The age must be a whole number.");
            }
            return _Age;
        }
    }
}