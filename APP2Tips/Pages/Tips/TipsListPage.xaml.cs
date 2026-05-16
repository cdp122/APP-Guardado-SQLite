using APP2Tips.Entities;
using APP2Tips.Managers;
using ManagersTips = APP2Tips.Managers.Tips;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace APP2Tips.Pages.Tips
{
    [QueryProperty(nameof(StackId), "stackId")]
    public partial class TipsListPage : ContentPage
    {
        private ManagersTips _tipsManager;
        private int _stackId;

        public TipsListPage()
        {
            InitializeComponent();
            _tipsManager = new ManagersTips();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadTips();
        }

        public string StackId
        {
            set
            {
                if (int.TryParse(value, out var id))
                {
                    _stackId = id;
                    LoadTips();
                }
            }
        }

        private void LoadTips()
        {
            try
            {
                var all = _tipsManager.GetAllTips();
                var filtered = all.FindAll(t => t.StackTechID == _stackId);
                TipsCollection.ItemsSource = filtered;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "No se pudieron cargar los tips: " + ex.Message, "OK");
            }
        }

        private async void OnAddTipClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"tipdetail?stackId={_stackId}");
        }

        private async void OnEditTipClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int tipId = (int)button.CommandParameter;
            await Shell.Current.GoToAsync($"tipdetail?stackId={_stackId}&tipId={tipId}&isEdit=true");
        }

        private async void OnDeleteTipClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int tipId = (int)button.CommandParameter;
            bool confirm = await DisplayAlert("Confirmar", "¿Eliminar este tip?", "Sí", "No");
            if (confirm)
            {
                try
                {
                    _tipsManager.DeleteTip(tipId);
                    LoadTips();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "No se pudo eliminar: " + ex.Message, "OK");
                }
            }
        }
    }
}