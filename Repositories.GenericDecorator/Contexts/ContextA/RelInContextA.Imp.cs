namespace Spike.Repositories.GenericDecorator.Contexts.ContextA
{
    public abstract class RelInContextAExtension<TEnclosedRel> : RelExtension<TEnclosedRel>, RelInContextA<TEnclosedRel>
        where TEnclosedRel : Relationship
    {
        protected RelInContextAExtension(TEnclosedRel component)
            : base(component)
        {
        }

        public override string Modelled
        {
            get { return base.Modelled + " " + this.ModelledExtA; }
        }

        public override string Planned
        {
            get { return base.Planned + " " + this.PlannedExtA; }
        }

        public string ModelledExtA { get; set; }

        public string PlannedExtA { get; set; }
    }

    public class RelInContextAImp : RelInContextAExtension<Relationship>
    {
        public static IRelInContextAFactory<Relationship> Factory = new RelInContextAFactory();

        public RelInContextAImp(Relationship component)
            : base(component)
        {
        }

        private class RelInContextAFactory : IRelInContextAFactory<Relationship>
        {
            public RelInContextA<Relationship> Create(Relationship relInput, RelInContextA contextInfo)
            {
                return new RelInContextAImp(relInput) { ModelledExtA = contextInfo.ModelledExtA, PlannedExtA = contextInfo.PlannedExtA };
            }
        }
    }
}
