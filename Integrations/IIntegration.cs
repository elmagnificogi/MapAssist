using MapAssist.Helpers;
using System;
using System.Windows.Forms;

namespace MapAssist.Integrations
{
    public interface IIntegration
    {
        string Name { get; }

        ToolStripItem[] ContextMenuItems { get; }

        UserControl ConfigurationControls { get; }

        void Initialize(byte[] buffer, ProcessContext context);

        void Run(ProcessContext context);
    }
}
