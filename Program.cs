using System;
using System.Linq;
using Spike.Repositories.GenericDecorator;
using Spike.Repositories.GenericDecorator.Contexts;
using Spike.Repositories.GenericDecorator.Contexts.ContextA;
using Spike.Repositories.GenericDecorator.Contexts.ContextB;

namespace Spike
{
    class Program
    {
        static void Main(string[] args)
        {
            var rels = new[] { new RelationshipImp { Modelled = "MInst_1", Planned = "POcrn_1" }, new RelationshipImp { Modelled = "MInst_2", Planned = "POcrn_2" } };

            Console.WriteLine("The source relationships:");
            Console.WriteLine(rels.Select(x => string.Format("{0} <-> {1}", x.Modelled, x.Planned)).DefaultIfEmpty("<Empty>").Aggregate((sum, next) => sum + "\n" + next));
            Console.WriteLine();

            IContextPopulator<RelInContextB<RelInContextA<Relationship>>> chainOfPopulators =
                new ContextAPopulator<Relationship>(RelInContextAImp.Factory).Then(
                new ContextBPopulator<RelInContextA<Relationship>>(RelInContextAThenContextBImp.Factory));

            var enhancedRels = chainOfPopulators.Populate(rels).ToList();

            Console.WriteLine("The end result:");
            Console.WriteLine(enhancedRels.Select(x => string.Format("{0} <-> {1}", x.Modelled, x.Planned)).DefaultIfEmpty("<Empty>").Aggregate((sum, next) => sum + "\n" + next));
            Console.WriteLine();

            Console.WriteLine("The end result (in Context A):");
            Console.WriteLine(enhancedRels.Select(x => string.Format("{0} <-> {1}", x.Enclosed.Modelled, x.Enclosed.Planned)).DefaultIfEmpty("<Empty>").Aggregate((sum, next) => sum + "\n" + next));
            Console.WriteLine();

            Console.WriteLine("The end result (in initial context):");
            Console.WriteLine(enhancedRels.Select(x => string.Format("{0} <-> {1}", x.Enclosed.Enclosed.Modelled, x.Enclosed.Enclosed.Planned)).DefaultIfEmpty("<Empty>").Aggregate((sum, next) => sum + "\n" + next));
            Console.WriteLine();
            
            Console.ReadKey();
        }
    }
}
