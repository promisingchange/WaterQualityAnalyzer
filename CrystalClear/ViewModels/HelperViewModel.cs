using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using MaterialDesignThemes.Wpf;

using CrystalClear.Domain;
using CrystalClear.DL;
using CrystalClear.DL.Models;

namespace CrystalClear.ViewModels
{
    public class HelperViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<EnvironmentalStandardViewModel> _environmentalStandards;
        private ObservableCollection<OxygenSolubilityViewModel> _oxygenSolubilities;
        private ObservableCollection<BaseIndex> _individualIndicesFor10Celsius;
        private ObservableCollection<BaseIndex> _individualIndicesFor20Celsius;
        private ObservableCollection<BaseIndex> _individualIndicesFor30Celsius;
        private ObservableCollection<BaseIndex> _weightCoefficientsFor10Celsius;
        private ObservableCollection<BaseIndex> _weightCoefficientsFor20Celsius;
        private ObservableCollection<BaseIndex> _weightCoefficientsFor30Celsius;
        private ObservableCollection<BaseIndex> _weightIndicesFor10Celsius;
        private ObservableCollection<BaseIndex> _weightIndicesFor20Celsius;
        private ObservableCollection<BaseIndex> _weightIndicesFor30Celsius;

        public HelperViewModel()
        {
            EnvironmentalStandards = new ObservableCollection<EnvironmentalStandardViewModel>();
            OxygenSolubilities = new ObservableCollection<OxygenSolubilityViewModel>();
            IndividualIndicesFor10Celsius = new ObservableCollection<BaseIndex>();
            IndividualIndicesFor20Celsius = new ObservableCollection<BaseIndex>();
            IndividualIndicesFor30Celsius = new ObservableCollection<BaseIndex>();
            WeightCoefficientsFor10Celsius = new ObservableCollection<BaseIndex>();
            WeightCoefficientsFor20Celsius = new ObservableCollection<BaseIndex>();
            WeightCoefficientsFor30Celsius = new ObservableCollection<BaseIndex>();
            WeightIndicesFor10Celsius = new ObservableCollection<BaseIndex>();
            WeightIndicesFor20Celsius = new ObservableCollection<BaseIndex>();
            WeightIndicesFor30Celsius = new ObservableCollection<BaseIndex>();
        }
        public ObservableCollection<EnvironmentalStandardViewModel> EnvironmentalStandards
        {
            get { return _environmentalStandards; }
            set
            {
                _environmentalStandards = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<OxygenSolubilityViewModel> OxygenSolubilities
        {
            get { return _oxygenSolubilities; }
            set
            {
                _oxygenSolubilities = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> IndividualIndicesFor10Celsius
        {
            get { return _individualIndicesFor10Celsius; }
            set
            {
                _individualIndicesFor10Celsius = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> IndividualIndicesFor20Celsius
        {
            get { return _individualIndicesFor20Celsius; }
            set
            {
                _individualIndicesFor20Celsius = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> IndividualIndicesFor30Celsius
        {
            get { return _individualIndicesFor30Celsius; }
            set
            {
                _individualIndicesFor30Celsius = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> WeightCoefficientsFor10Celsius
        {
            get { return _weightCoefficientsFor10Celsius; }
            set
            {
                _weightCoefficientsFor10Celsius = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> WeightCoefficientsFor20Celsius
        {
            get { return _weightCoefficientsFor20Celsius; }
            set
            {
                _weightCoefficientsFor20Celsius = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> WeightCoefficientsFor30Celsius
        {
            get { return _weightCoefficientsFor30Celsius; }
            set
            {
                _weightCoefficientsFor30Celsius = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> WeightIndicesFor10Celsius
        {
            get { return _weightIndicesFor10Celsius; }
            set
            {
                _weightIndicesFor10Celsius = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> WeightIndicesFor20Celsius
        {
            get { return _weightIndicesFor20Celsius; }
            set
            {
                _weightIndicesFor20Celsius = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BaseIndex> WeightIndicesFor30Celsius
        {
            get { return _weightIndicesFor30Celsius; }
            set
            {
                _weightIndicesFor30Celsius = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
