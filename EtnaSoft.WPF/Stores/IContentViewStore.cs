using System;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Stores
{
    public interface IContentViewStore
    {
        event Action ContentViewChanged;
        ContentViewModel CurrentContentView { get; set; }
        void Dispose();
    }
}