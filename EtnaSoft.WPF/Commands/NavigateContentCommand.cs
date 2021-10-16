using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtnaSoft.WPF.Services;
using EtnaSoft.WPF.Stores;

namespace EtnaSoft.WPF.Commands
{
    public class NavigateContentCommand : BaseCommand
    {
        private readonly IContentViewFactory _contentViewFactory;
        private readonly IContentViewStore _contentStore;

        public NavigateContentCommand(IContentViewFactory contentViewFactory, IContentViewStore contentStore)
        {
            _contentViewFactory = contentViewFactory;
            _contentStore = contentStore;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ContentViewType type)
            {
                _contentStore.CurrentContentView = _contentViewFactory.CreateContentViewModel(type);
            }
        }
    }
}
