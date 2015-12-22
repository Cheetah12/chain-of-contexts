namespace Spike.Repositories.GenericDecorator.Contexts.ContextB
{
    public abstract class RelInContextBExtension<TEnclosedRel> : RelExtension<TEnclosedRel>, RelInContextB<TEnclosedRel>
        where TEnclosedRel : Relationship
    {
        protected RelInContextBExtension(TEnclosedRel component)
            : base(component)
        {
        }

        public override string Modelled
        {
            get { return base.Modelled + " " + this.ModelledExtB; }
        }

        public override string Planned
        {
            get { return base.Planned + " " + this.PlannedExtB; }
        }

        public string ModelledExtB { get; set; }

        public string PlannedExtB { get; set; }
    }

    public class RelInContextBImp : RelInContextBExtension<Relationship>
    {
        public RelInContextBImp(Relationship component)
            : base(component)
        {
        }
    }

    public abstract class RelInContextAThenContextBExtension<TRelInContextA> : RelInContextBExtension<TRelInContextA>
        where TRelInContextA : RelInContextA
    {
        protected RelInContextAThenContextBExtension(TRelInContextA component)
            : base(component)
        {
        }
    }

    public class RelInContextAThenContextBImp : RelInContextAThenContextBExtension<RelInContextA<Relationship>>
    {
        public static IRelInContextBFactory<RelInContextA<Relationship>> Factory = new RelInContextAThenContextBFactory();

        public RelInContextAThenContextBImp(RelInContextA<Relationship> component)
            : base(component)
        {
        }

        private class RelInContextAThenContextBFactory : IRelInContextBFactory<RelInContextA<Relationship>>
        {
            public RelInContextB<RelInContextA<Relationship>> Create(RelInContextA<Relationship> relInput, RelInContextB contextInfo)
            {
                return new RelInContextAThenContextBImp(relInput) { ModelledExtB = contextInfo.ModelledExtB, PlannedExtB = contextInfo.PlannedExtB };
            }
        }
    }
}
