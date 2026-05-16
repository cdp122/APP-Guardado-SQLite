using APP2Tips.Entities;
using APP2Tips.Managers;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace APP2Tips.Pages
{
    public partial class StackTechListPage : ContentPage
    {
        private StackTechs _stackTechManager;

        public StackTechListPage()
        {
            InitializeComponent();
            _stackTechManager = new StackTechs();
        }

        private async void OnStackCardTapped(object sender, EventArgs e)
        {
            // sender is the StackLayout; get CommandParameter from gesture
            if (sender is VisualElement ve && ve.BindingContext is APP2Tips.Entities.StackTech st)
            {
                int stackId = st.ID;
                await Shell.Current.GoToAsync($"tipslist?stackId={stackId}");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadStacks();
        }

        private void LoadStacks()
        {
            try
            {
                List<StackTech> stacks = _stackTechManager.GetAllStack();
                StacksCollection.ItemsSource = stacks;
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "No se pudieron cargar los stacks: " + ex.Message, "OK");
            }
        }

        private async void OnAddStackClicked(object sender, EventArgs e)
        {
            // Use Shell navigation (route registered in AppShell)
            await Shell.Current.GoToAsync("stacktechdetail");
        }

        private async void OnViewStackClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int stackId = (int)button.CommandParameter;
            await Shell.Current.GoToAsync($"stacktechdetail?stackId={stackId}");
        }

        private async void OnEditStackClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int stackId = (int)button.CommandParameter;
            await Shell.Current.GoToAsync($"stacktechdetail?stackId={stackId}&isEdit=true");
        }

        private async void OnDeleteStackClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int stackId = (int)button.CommandParameter;

            bool confirm = await DisplayAlert("Confirmar", "¿Está seguro de que desea eliminar este stack?", "Sí", "No");
            if (confirm)
            {
                try
                {
                    _stackTechManager.DeleteStack(stackId);
                    LoadStacks();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "No se pudo eliminar el stack: " + ex.Message, "OK");
                }
            }
        }
    }
}