using Entities.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorDataManagerUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Category_cbx.SelectedIndex = 0;
            DeviceClass_cbx.SelectedIndex = 0;
            ErrorDataGridView.AutoGenerateColumns = false;
            ErrorDataGridView.Columns["Id"].DataPropertyName = "Id";
            ErrorDataGridView.Columns["Code"].DataPropertyName = "Code";
            ErrorDataGridView.Columns["Description"].DataPropertyName = "Description";
            ErrorDataGridView.Columns["Category"].DataPropertyName = "Category";
            ErrorDataGridView.Columns["DeviceClassName"].DataPropertyName = "DeviceClassName";
            ErrorDataGridView.Columns["Tag"].DataPropertyName = "Tag";
            ErrorDataGridView.Columns["CreatedBy"].DataPropertyName = "CreatedBy";
            ErrorDataGridView.Columns["CreateDate"].DataPropertyName = "CreateDate";
            ErrorDataGridView.Columns["UpdatedBy"].DataPropertyName = "UpdatedBy";
            ErrorDataGridView.Columns["UpdateDate"].DataPropertyName = "UpdateDate";
            Update_btn.Enabled = false;
            Delete_btn.Enabled = false;
        }

        private async void Search_btn_Click(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                var obj = new SearchErrorRequest
                {
                    ErrorDataList = new List<ErrorData> { new ErrorData()
                    {
                        Category = Category_cbx.Text,
                        Code = ErrorCode_tbx.Text,
                        Description = Description_tbx.Text,
                        DeviceClassName = DeviceClass_cbx.Text,
                        Tag = Tag_tbx.Text,
                        CreateDate = DateTime.Now,
                        CreatedBy = ""
                    }
                    }
                };
                var responseStr = await CallApiPostMethodAsync(client, obj, "search");
                var searchResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SearchResult>(responseStr);
                ErrorDataGridView.SelectionChanged -= new System.EventHandler(this.ErrorDataGridView_SelectionChanged);
                ErrorDataGridView.DataSource = null;
                ErrorDataGridView.DataSource = searchResult.Data;
                ErrorDataGridView.ClearSelection();
                ErrorDataGridView.SelectionChanged += new System.EventHandler(this.ErrorDataGridView_SelectionChanged);
                var messageBoxResult = MessageBox.Show("Form temizlensin mi?", "" , MessageBoxButtons.YesNo);
                if (messageBoxResult == DialogResult.OK)
                {
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                // need to return ex.message for display.
                Description_tbx.Text = ex.ToString();
            }
        }

        private async void Add_btn_Click(object sender, EventArgs e)
        {
            var errorData = new ErrorData()
            {
                Category = Category_cbx.Text,
                Code = ErrorCode_tbx.Text,
                Description = Description_tbx.Text,
                DeviceClassName = DeviceClass_cbx.Text,
                Tag = Tag_tbx.Text,
                CreateDate = DateTime.Now,
                CreatedBy = ""
            };
            var anyFormFieldsEmpty = AnyFormFieldsIsEmpty();
            if (anyFormFieldsEmpty)
            {
                var errorMessageDialogResult = MessageBox.Show("Boş alanları doldurunuz ve gerekli seçimleri yapınız.");
            }
            else
            {
                var response = await AddErrorData(new ErrorData[] { errorData });
                var dialogResult = MessageBox.Show(response.ResponseDescription);
                if (dialogResult == DialogResult.OK)
                {
                    ResetForm();
                }
            }
        }

        private async Task<ErrorDataResponse> AddErrorData(ErrorData[] errorDataArray)
        {
            try
            {
                HttpClient client = new HttpClient();
                var obj = new ErrorDataRequest
                {
                    ErrorDataArray = errorDataArray
                };
                var responseStr = await CallApiPostMethodAsync(client, obj, "add");
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorDataResponse>(responseStr);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void Update_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ErrorDataGridView.SelectedRows.Count != 0)
                {
                    var selectedId = (ErrorDataGridView.SelectedRows[0].DataBoundItem as ErrorData).Id;
                    var anyFormFieldsEmpty = AnyFormFieldsIsEmpty();
                    if (anyFormFieldsEmpty)
                    {
                        var errorMessageDialogResult = MessageBox.Show("Boş alanları doldurunuz ve gerekli seçimleri yapınız.");
                    }
                    else
                    {
                        HttpClient client = new HttpClient();
                        var obj = new ErrorDataRequest
                        {
                            ErrorDataArray = new ErrorData[] { new ErrorData()
                            {
                                Id = selectedId,
                                Category = Category_cbx.Text,
                                Code = ErrorCode_tbx.Text,
                                Description = Description_tbx.Text,
                                DeviceClassName = DeviceClass_cbx.Text,
                                Tag = Tag_tbx.Text,
                                UpdateDate = DateTime.Now,
                                UpdatedBy = ""

                            }
                            }
                        };
                        var responseStr = await CallApiPutMethodAsync(client, obj, "update");
                        var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorDataResponse>(responseStr);
                        var dialogResult = MessageBox.Show(response.ResponseDescription);
                        if (dialogResult == DialogResult.OK)
                        {
                            ResetForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void Delete_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (ErrorDataGridView.SelectedRows.Count != 0)
                {
                    var selected = (ErrorDataGridView.SelectedRows[0].DataBoundItem as ErrorData);
                    var selectedId = selected.Id;

                    var anyFormFieldsEmpty = AnyFormFieldsIsEmpty();
                    if (anyFormFieldsEmpty)
                    {
                        var errorMessageDialogResult = MessageBox.Show("Lütfen bir seçim yapınız.");
                    }
                    else
                    {
                        HttpClient client = new HttpClient();
                        var formUniqueKey = Category_cbx.Text.Trim() + DeviceClass_cbx.Text.Trim() + ErrorCode_tbx.Text.Trim();
                        var selectedRowUniqueKey = selected.Category.Trim() + selected.DeviceClassName.Trim() + selected.Code.Trim();
                        if (selectedRowUniqueKey == formUniqueKey)
                        {
                            var responseStr = await CallApiDeleteMethodAsync(client, selectedId, "delete");
                            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorDataResponse>(responseStr);
                            var dialogResult = MessageBox.Show(response.ResponseDescription);
                            if (dialogResult == DialogResult.OK)
                            {
                                ResetForm();
                            }
                        }
                        else
                        {
                            var errorMessageDialogResult = MessageBox.Show("Lütfen bir seçim yapınız.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ErrorDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (ErrorDataGridView.SelectedRows.Count != 0)
                {
                    var selectedRow = ErrorDataGridView.SelectedRows[0].DataBoundItem as ErrorData;
                    ErrorCode_tbx.Text = selectedRow.Code.ToString();
                    Description_tbx.Text = selectedRow.Description.ToString();
                    Tag_tbx.Text = selectedRow.Tag.ToString();
                    try
                    {
                        Category_cbx.SelectedIndex = Category_cbx.FindStringExact(selectedRow.Category.ToString());
                        DeviceClass_cbx.SelectedIndex = DeviceClass_cbx.FindStringExact(selectedRow.DeviceClassName.ToString());
                    }
                    catch (Exception ex)
                    {
                        var dialogResult = MessageBox.Show("Seçilen data düzgün değil, lütfen seçiminizi düzeltiniz.");
                    }
                    Update_btn.Enabled = true;
                    Delete_btn.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Reset_btn_Click(object sender, EventArgs e)
        {
            ResetForm();
            ErrorDataGridView.DataSource = null;
        }
        private void ResetForm()
        {
            foreach (var item in this.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Clear();
                }
                if (item is ComboBox)
                {
                    ((ComboBox)item).SelectedIndex = 0;
                }
                if (item is DataGridView)
                {
                    ((DataGridView)item).Refresh();
                }
            }
            Category_cbx.SelectedIndex = 0;
            DeviceClass_cbx.SelectedIndex = 0;
            Update_btn.Enabled = false;
            Delete_btn.Enabled = false;
        }

        private async Task<string> CallApiPostMethodAsync(HttpClient client, BaseRequest obj, string action)
        {
            using (HttpResponseMessage response = await client.PostAsJsonAsync(new Uri($"https://localhost:7139/api/error/{action}"), obj))
            {
                using (HttpContent content = response.Content)
                {
                    // need these to return to Form for display
                    return await ProcessResponse(response, content);
                }
            }
        }

        private async Task<String> CallApiPutMethodAsync(HttpClient client, BaseRequest obj, string action)
        {
            using (HttpResponseMessage response = await client.PutAsJsonAsync(new Uri($"https://localhost:7139/api/error/{action}"), obj))
            {
                using (HttpContent content = response.Content)
                {
                    // need these to return to Form for display
                    return await ProcessResponse(response, content);
                }
            }
        }

        private async Task<String> CallApiDeleteMethodAsync(HttpClient client, long id, string action)
        {
            using (HttpResponseMessage response = await client.DeleteAsync(new Uri($"https://localhost:7139/api/error/{action}?id={id}").ToString()))
            {
                using (HttpContent content = response.Content)
                {
                    // need these to return to Form for display
                    return await ProcessResponse(response, content);
                }
            }
        }

        private async Task<string> ProcessResponse(HttpResponseMessage response, HttpContent content)
        {
            string resultString = await content.ReadAsStringAsync();
            string reasonPhrase = response.ReasonPhrase;
            HttpResponseHeaders headers = response.Headers;
            HttpStatusCode code = response.StatusCode;

            return resultString;
        }

        private bool AnyFormFieldsIsEmpty()
        {
            var isDeviceClassNameEmpty = DeviceClass_cbx.SelectedIndex == 0 || DeviceClass_cbx.Text == string.Empty;
            var isErrorCodeTbxEmpty = ErrorCode_tbx.Text == string.Empty;
            var isDescriptionTbxEmpty = Description_tbx.Text == string.Empty;
            var isCategoryEmpty = Category_cbx.Text == string.Empty;
            return isDeviceClassNameEmpty || isErrorCodeTbxEmpty || isDescriptionTbxEmpty || isCategoryEmpty;
        }

        private async void BulkInsert_btn_Click(object sender, EventArgs e)
        {
            try
            {


                var fileContent = string.Empty;
                var filePath = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Get the path of specified file
                        filePath = openFileDialog.FileName;

                        //Read the contents of the file into a stream
                        var fileStream = openFileDialog.OpenFile();

                        using (StreamReader reader = new StreamReader(fileStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }
                    }
                }
                var lines = fileContent.Split(Environment.NewLine.ToCharArray());
                StringBuilder sb = new StringBuilder();
                Dictionary<string, ErrorDataResponse> insertResults = new Dictionary<string, ErrorDataResponse>();
                Dictionary<string, ErrorData> insertErrorDataArray = new Dictionary<string, ErrorData>();
                foreach (var line in lines)
                {
                    if (line.Length == 0) continue;
                    var category = string.Empty;
                    var deviceClassName = string.Empty;
                    var tokens = line.Split(new string[] { "#;#" }, StringSplitOptions.None);
                    var errorCode = tokens[0];
                    var description = tokens[1];
                    var tag = tokens.Length > 2 ? tokens[2] : string.Empty;
                    SetCategory(ref category, ref deviceClassName, errorCode);
                    var errorData = new ErrorData();
                    errorData.Description = description.Trim();
                    errorData.Category = category.Trim();
                    errorData.DeviceClassName = deviceClassName.Trim();
                    errorData.Code = errorCode.Trim();
                    errorData.Tag = tag.Trim();
                    errorData.CreateDate = DateTime.Now;
                    errorData.CreatedBy = "";
                    var resultDataKey = errorData.Category.Trim() + errorData.DeviceClassName.Trim() + errorData.Code.Trim();
                    if (!insertErrorDataArray.ContainsKey(resultDataKey))
                    {
                        insertErrorDataArray.Add(resultDataKey, errorData);
                        //if (response != null)
                        //{
                        //    insertResults[resultDataKey] = response;
                        //}
                        //else
                        //{
                        //    insertResults[resultDataKey] = new ErrorDataResponse()
                        //    {
                        //        ResponseCode = "3",
                        //        ResponseDescription = "Response null."
                        //    };
                        //}
                    }
                }
                var response = await AddErrorData(insertErrorDataArray.Values.ToArray());
                MessageBox.Show(response.ResponseDescription);
            }
            //foreach (var item in insertResults)
            //{
            //    sb.AppendLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
            //}
            //File.WriteAllText(filePath + ".BulkInsertLog", sb.ToString());            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static void SetCategory(ref string category, ref string deviceClassName, string errorCode)
        {
            var errorCodeintValue = Convert.ToInt32(errorCode);
            category = "XFS";
            if (errorCodeintValue <= -10000)
            {
                category = "Simax";
                errorCodeintValue = errorCodeintValue % 10000;
            }

            if (errorCodeintValue <= -1 && errorCodeintValue > -100)
            {
                deviceClassName = "PTR";
            }
            if (errorCodeintValue <= -100 && errorCodeintValue > -200)
            {
                deviceClassName = "PTR";
            }
            else if (errorCodeintValue <= -200 && errorCodeintValue > -300)
            {
                deviceClassName = "IDC";
            }
            else if (errorCodeintValue <= -300 && errorCodeintValue > -400)
            {
                deviceClassName = "CDM";
            }
            else if (errorCodeintValue <= -400 && errorCodeintValue > -500)
            {
                deviceClassName = "PIN";
            }
            else if (errorCodeintValue <= -500 && errorCodeintValue > -600)
            {
                deviceClassName = "CHK";
            }
            else if (errorCodeintValue <= -600 && errorCodeintValue > -700)
            {
                deviceClassName = "DEP";
            }
            else if (errorCodeintValue <= -700 && errorCodeintValue > -800)
            {
                deviceClassName = "TTU";
            }
            else if (errorCodeintValue <= -800 && errorCodeintValue > -900)
            {
                deviceClassName = "SIU";
            }
            else if (errorCodeintValue <= -900 && errorCodeintValue > -1000)
            {
                deviceClassName = "VDM";
            }
            else if (errorCodeintValue <= -1000 && errorCodeintValue > -1100)
            {
                deviceClassName = "CAM";
            }
            else if (errorCodeintValue <= -1100 && errorCodeintValue > -1200)
            {
                deviceClassName = "ALM";
            }
            else if (errorCodeintValue <= -1200 && errorCodeintValue > -1300)
            {
                deviceClassName = "CEU";
            }
            else if (errorCodeintValue <= -1300 && errorCodeintValue > -1400)
            {
                deviceClassName = "CIM";
            }
            else if (errorCodeintValue <= -1400 && errorCodeintValue > -1500)
            {
                deviceClassName = "CRD";
            }
            else if (errorCodeintValue <= -1500 && errorCodeintValue > -1600)
            {
                deviceClassName = "BCR";
            }
            else if (errorCodeintValue <= -1600 && errorCodeintValue > -1700)
            {
                deviceClassName = "IPM";
            }
            else if (errorCodeintValue <= -1700 && errorCodeintValue > -1800)
            {
                deviceClassName = "BIO";
            }
        }
    }
}
