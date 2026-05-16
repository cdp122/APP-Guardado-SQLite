using APP2Tips.Entities;
using APP2Tips.Managers;
using ManagersTips = APP2Tips.Managers.Tips;
using Microsoft.Maui.Controls;
using System;

namespace APP2Tips.Pages.Tips
{
    [QueryProperty(nameof(StackId), "stackId")]
    [QueryProperty(nameof(TipId), "tipId")]
    [QueryProperty(nameof(IsEdit), "isEdit")]
    public partial class TipDetailPage : ContentPage
    {
        private ManagersTips _tipsManager;
        private int? _tipId;
        private int _stackId;
        private bool _isEdit;

        public TipDetailPage()
        {
            InitializeComponent();
            _tipsManager = new APP2Tips.Managers.Tips();
        }

        public string StackId
        {
            set { int.TryParse(value, out _stackId); }
        }

        public string TipId
        {
            set { if (int.TryParse(value, out var id)) { _tipId = id; LoadTip(); } }
        }

        public string IsEdit
        {
            set { _isEdit = string.Equals(value, "true", StringComparison.OrdinalIgnoreCase); }
        }

        private void LoadTip()
        {
            if (!_tipId.HasValue) return;
            var tip = _tipsManager.GetTip(_tipId.Value);
            if (tip != null)
            {
                TituloEntry.Text = tip.TituloTip;
                DescripcionEntry.Text = tip.DescripcionTip;
                CodigoEditor.Text = tip.CodigoTip;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TituloEntry.Text))
                {
                    await DisplayAlert("Error", "El título es obligatorio.", "OK");
                    return;
                }

                if (_isEdit && _tipId.HasValue)
                {
                    _tipsManager.UpdateTip(_tipId.Value, _stackId, TituloEntry.Text, DescripcionEntry.Text, CodigoEditor.Text);
                }
                else
                {
                    _tipsManager.CreateTip(_stackId, TituloEntry.Text, DescripcionEntry.Text, CodigoEditor.Text);
                }

                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}