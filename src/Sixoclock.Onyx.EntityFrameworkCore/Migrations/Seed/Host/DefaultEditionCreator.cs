using System.Linq;
using Abp.Application.Features;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Editions;
using Sixoclock.Onyx.EntityFrameworkCore;
using Sixoclock.Onyx.Features;

namespace Sixoclock.Onyx.Migrations.Seed.Host
{
    public class DefaultEditionCreator
    {
        private readonly OnyxDbContext _context;

        public DefaultEditionCreator(OnyxDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateEditions();
        }

        private void CreateEditions()
        {
            var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition == null)
            {
                defaultEdition = new SubscribableEdition { Name = EditionManager.DefaultEditionName, DisplayName = EditionManager.DefaultEditionName };
                _context.Editions.Add(defaultEdition);
                _context.SaveChanges();

                /* Add desired features to the standard edition, if wanted... */
            }

            //var proEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.ProEditionName);
            //if (proEdition == null)
            //{
            //    proEdition = new SubscribableEdition { Name = EditionManager.ProEditionName, DisplayName = EditionManager.ProEditionName };
            //    _context.Editions.Add(proEdition);
            //    _context.SaveChanges();

            //    /* Add desired features to the standard edition, if wanted... */
            //}

            //var integratorEditionName = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.IntegratorEditionName);
            //if (integratorEditionName == null)
            //{
            //    integratorEditionName = new SubscribableEdition { Name = EditionManager.IntegratorEditionName, DisplayName = EditionManager.IntegratorEditionName };
            //    _context.Editions.Add(integratorEditionName);
            //    _context.SaveChanges();

            //    /* Add desired features to the standard edition, if wanted... */
            //}

#if FEATURE_SIGNALR
            if (defaultEdition.Id > 0)
            {
                CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.ChatFeature, true);
                CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.TenantToTenantChatFeature, true);
                CreateFeatureIfNotExists(defaultEdition.Id, AppFeatures.TenantToHostChatFeature, true);
            }
#endif
        }

        private void CreateFeatureIfNotExists(int editionId, string featureName, bool isEnabled)
        {
            var defaultEditionChatFeature = _context.EditionFeatureSettings.IgnoreQueryFilters()
                                                        .FirstOrDefault(ef => ef.EditionId == editionId && ef.Name == featureName);

            if (defaultEditionChatFeature == null)
            {
                _context.EditionFeatureSettings.Add(new EditionFeatureSetting
                {
                    Name = featureName,
                    Value = isEnabled.ToString(),
                    EditionId = editionId
                });
            }
        }
    }
}