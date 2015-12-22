using System;
using System.Linq;

namespace Spike.Repositories.GenericDecorator
{
    public interface Relationship
    {
        string Modelled { get; }
        string Planned { get; }
    }

    public interface Relationship<out TRel> : Relationship
        where TRel : Relationship
    {
        TRel Enclosed { get; }
    }

    public class RelationshipImp : Relationship
    {
        public string Modelled { get; set; }
        public string Planned { get; set; }
    }

    public abstract class RelExtension : Relationship
    {
        private readonly Relationship component;

        protected RelExtension(Relationship component)
        {
            this.component = component;
        }

        public virtual string Modelled
        {
            get { return this.component.Modelled; }
        }

        public virtual string Planned
        {
            get { return this.component.Planned; }
        }
    }

    public abstract class RelExtension<TEnclosedRel> : RelExtension, Relationship<TEnclosedRel>
        where TEnclosedRel : Relationship
    {
        private readonly TEnclosedRel component;

        protected RelExtension(TEnclosedRel component)
            : base(component)
        {
            this.component = component;
        }

        public TEnclosedRel Enclosed
        {
            get { return this.component; }
        }
    }
}
