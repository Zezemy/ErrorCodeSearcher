using Entities.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                ErrorDataGridView.DataSource = searchResult.Data;
            }
            catch (Exception ex)
            {
                // need to return ex.message for display.
                Description_tbx.Text = ex.ToString();
            }
        }
        private async void Add_btn_Click(object sender, EventArgs e)
        {
            try
            {
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
                        ErrorData = new ErrorData()
                        {
                            Category = Category_cbx.Text,
                            Code = ErrorCode_tbx.Text,
                            Description = Description_tbx.Text,
                            DeviceClassName = DeviceClass_cbx.Text,
                            Tag = Tag_tbx.Text,
                            CreateDate = DateTime.Now,
                            CreatedBy = ""
                        }
                    };
                    var responseStr = await CallApiPostMethodAsync(client, obj, "add");
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorDataResponse>(responseStr);
                    var dialogResult = MessageBox.Show(response.ResponseDescription);
                    if (dialogResult == DialogResult.OK)
                    {
                        ResetForm();
                    }
                }
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
                            ErrorData = new ErrorData()
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
                    var selectedId = (ErrorDataGridView.SelectedRows[0].DataBoundItem as ErrorData).Id;
                    HttpClient client = new HttpClient();
                    var responseStr = await CallApiDeleteMethodAsync(client, selectedId, "delete");
                    var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorDataResponse>(responseStr);
                    var dialogResult = MessageBox.Show(response.ResponseDescription);
                    if (dialogResult == DialogResult.OK)
                    {
                        ResetForm();
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
                    try
                    {
                        Category_cbx.SelectedIndex = Category_cbx.FindStringExact(selectedRow.Category.ToString());
                        DeviceClass_cbx.SelectedIndex = DeviceClass_cbx.FindStringExact(selectedRow.DeviceClassName.ToString());
                    }
                    catch (Exception ex)
                    {
                        var dialogResult = MessageBox.Show("Seçilen data düzgün değil, lütfen seçiminizi düzeltiniz.");
                    }
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
        }
        private async Task<string> ProcessResponse(HttpResponseMessage response, HttpContent content)
        {
            string resultString = await content.ReadAsStringAsync();
            string reasonPhrase = response.ReasonPhrase;
            HttpResponseHeaders headers = response.Headers;
            HttpStatusCode code = response.StatusCode;

            return resultString;
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
        private bool AnyFormFieldsIsEmpty()
        {
            var isDeviceClassNameEmpty = DeviceClass_cbx.SelectedIndex == 0 || DeviceClass_cbx.Text == string.Empty;
            var isErrorCodeTbxEmpty = ErrorCode_tbx.Text == string.Empty;
            var isDescriptionTbxEmpty = Description_tbx.Text == string.Empty;
            var isCategoryEmpty = Category_cbx.Text == string.Empty;
            return isDeviceClassNameEmpty || isErrorCodeTbxEmpty || isDescriptionTbxEmpty || isCategoryEmpty;
        }
    }
}
