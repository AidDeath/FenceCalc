using FenceCalc.Helpers.Commands;
using FenceCalc.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FenceCalc.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Title = "Fence calculcator";
            SpaceBetweenWood = 10;
            WoodWidth = 92;
            FenceWidth = 1600;

            SavedFences = new();

            AddSavedFenceCommand = new RelayCommand(OnAddSavedFenceCommandExecuted);
            RemoveSavedFence = new RelayCommand(OnRemoveSavedFenceExecuted, CanRemoveSavedFenceExecute);
        }

        private ObservableCollection<SavedFence> _saverFences;

        public ObservableCollection<SavedFence> SavedFences
        {
            get => _saverFences;
            set => SetProperty(ref _saverFences, value);
        }

        private SavedFence _selectedSavedFence;

        public SavedFence SelectedSavedFence
        {
            get => _selectedSavedFence;
            set => SetProperty(ref _selectedSavedFence, value);
        }

        private int _spaceBetweenWood;

        public int SpaceBetweenWood
        {
            get => _spaceBetweenWood;
            set
            {
                SetProperty(ref _spaceBetweenWood, value);
                CalculateFence();
            }
        }

        private int _fenceWidth;

        public int FenceWidth
        {
            get => _fenceWidth;
            set
            {
                SetProperty(ref _fenceWidth, value);
                CalculateFence();
            }
        }

        private int _woodWidth;

        public int WoodWidth
        {
            get => _woodWidth;
            set
            {
                SetProperty(ref _woodWidth, value);
                CalculateFence();
            }
        }

        private int _sideSpaceWidth;

        public int SideSpaceWidth
        {
            get => _sideSpaceWidth;
            set => SetProperty(ref _sideSpaceWidth, value);
        }

        private int _woodCount;

        public int WoodCount
        {
            get => _woodCount;
            set => SetProperty(ref _woodCount, value);
        }

        private int _allSavedWoodsCount;

        public int AllSavedWoodsCount
        {
            get => _allSavedWoodsCount;
            set => SetProperty(ref _allSavedWoodsCount, value);
        }

        private void CalculateFence()
        {
            WoodCount = FenceWidth / (WoodWidth + SpaceBetweenWood);

            var sideSpace = (FenceWidth - (WoodCount * (WoodWidth + SpaceBetweenWood) - SpaceBetweenWood)) / 2.0d;

            SideSpaceWidth = (int)Math.Round(sideSpace);
        }

        public IRaisedCommand AddSavedFenceCommand { get; }

        private void OnAddSavedFenceCommandExecuted(object obj)
        {
            var fence = new SavedFence
            {
                FenceWidth = FenceWidth,
                SideSpaceWidth = SideSpaceWidth,
                SpaceBetweenWoods = SpaceBetweenWood,
                WoodsCount = WoodCount
            };

            SavedFences.Add(fence);
            recalcAllSavedWoodsCount();
        }

        public IRaisedCommand RemoveSavedFence { get; }

        private void OnRemoveSavedFenceExecuted(object obj)
        {
            SavedFences.Remove(SelectedSavedFence);
            recalcAllSavedWoodsCount();
        }

        private bool CanRemoveSavedFenceExecute(object obj)
        {
            return SelectedSavedFence is not null;
        }

        private void recalcAllSavedWoodsCount()
        {
            if (SavedFences is not null && SavedFences.Count > 0)
                AllSavedWoodsCount = SavedFences.Select(fence => fence.WoodsCount).Aggregate((a, b) => a + b);
            else AllSavedWoodsCount = 0;
        }
    }
}