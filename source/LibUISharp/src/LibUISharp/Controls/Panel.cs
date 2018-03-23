﻿using System;
using LibUISharp.Internal;

namespace LibUISharp.Controls
{
    public abstract class Panel : ContainerControl<PanelItemCollection, Panel>
    {
        protected Panel(Orientation orientation = Orientation.Horizontal)
        {
            Handle = LibUIAPI.NewPanel(orientation);
            Orientation = orientation;
        }

        public Orientation Orientation { get; }

        public bool Padding
        {
            get => LibUIAPI.PanelGetPadding(Handle);
            set => LibUIAPI.PanelSetPadding(Handle, value);
        }
    }

    public sealed class HPanel : Panel
    {
        public HPanel() : base(Orientation.Horizontal) { }
    }

    public sealed class VPanel : Panel
    {
        public VPanel() : base(Orientation.Horizontal) { }
    }
    
    public class PanelItemCollection : ControlCollection<Panel>
    {
        public PanelItemCollection(Panel uiParent) : base(uiParent) { }

        public override bool Remove(Control item)
        {
            LibUIAPI.PanelDelete(Owner.Handle, item.Index);
            return base.Remove(item);
        }

        public override void Add(Control child) => Add(child, false);

        public virtual void Add(Control child, bool stretches)
        {
            if (Contains(child))
                throw new InvalidOperationException("cannot add the same control.");
            if(child == null) return;
            LibUIAPI.PanelAppend(Owner.Handle, child.Handle, stretches);
            base.Add(child);
        }
    }
}