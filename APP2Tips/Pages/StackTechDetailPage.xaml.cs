using APP2Tips.Entities;
using APP2Tips.Managers;
using Microsoft.Maui.Controls;
using System;

namespace APP2Tips.Pages
{
    [QueryProperty("StackId", "stackId")]
    [QueryProperty("IsEdit", "isEdit")]
    public partial class StackTechDetailPage : ContentPage
    {
        private StackTechs _stackTechManager;
        private int? _stackId;
        private bool _isEditMode;

        public StackTechDetailPage()
        {
            InitializeComponent();
            _stackTechManager = new StackTechs();
        }

        // Properties used by Shell query parameters
        public string StackId
        {
            set
            {
                if (int.TryParse(value, out var id))
                {
                    _stackId = id;
                    // If currently in edit mode, load details now; otherwise load for view only
                    LoadStackDetails(id);
                    SetUiMode();
                }
            }
        }

        public string IsEdit
        {
            set
            {
                _isEditMode = string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
                SetUiMode();
                if (_stackId.HasValue)
                    LoadStackDetails(_stackId.Value);
            }
        }

        private void LoadStackDetails(int stackId)
        {
            try
            {
                StackTech stack = _stackTechManager.GetStack(stackId);
                if (stack != null)
                {
                    NombreEntry.Text = stack.NombreStack;
                    DescripcionEditor.Text = stack.DescripcionStack;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "No se pudieron cargar los detalles del stack: " + ex.Message, "OK");
            }
        }

        private void SetUiMode()
        {
            // If not edit mode, disable entries and hide save button to act as view-only
            bool editable = _isEditMode || !_stackId.HasValue;
            SaveButton.IsVisible = editable;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NombreEntry.Text))
                {
                    await DisplayAlert("Error", "El nombre del stack es obligatorio.", "OK");
                    return;
                }

                if (_isEditMode && _stackId.HasValue)
                {
                    _stackTechManager.UpdateStack(_stackId.Value, NombreEntry.Text, DescripcionEditor.Text);
                }
                else
                {
                    _stackTechManager.CreateStack(NombreEntry.Text, DescripcionEditor.Text);
                }

                await DisplayAlert("Éxito", "El stack ha sido guardado correctamente.", "OK");
                // Return using Shell navigation
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo guardar el stack: " + ex.Message, "OK");
            }
        }
    }
}