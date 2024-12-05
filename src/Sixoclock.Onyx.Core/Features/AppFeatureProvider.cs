using Abp.Application.Features;
using Abp.Localization;
using Abp.Runtime.Validation;
using Abp.UI.Inputs;

namespace Sixoclock.Onyx.Features
{
    public class AppFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            context.Create(
                AppFeatures.MaxUserCount,
                defaultValue: "0", //0 = unlimited
                displayName: L("MaximumUserCount"),
                description: L("MaximumUserCount_Description"),
                inputType: new SingleLineStringInputType(new NumericValueValidator(0, int.MaxValue))
            )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
            {
                ValueTextNormalizer = value => value == "0" ? L("Unlimited") : new FixedLocalizableString(value),
                IsVisibleOnPricingTable = true
            };

            context.Create(
               AppFeatures.MaxCustomerCount,
               defaultValue: "0", //0 = unlimited
               displayName: L("MaximumCustomerCount"),
               description: L("MaximumCustomerCount_Description"),
               inputType: new SingleLineStringInputType(new NumericValueValidator(0, int.MaxValue))
           )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
           {
               ValueTextNormalizer = value => value == "0" ? L("Unlimited") : new FixedLocalizableString(value),
               IsVisibleOnPricingTable = true
           };

            #region ######## Example Features - You can delete them #########

            //context.Create(
            //    AppFeatures.TestCheckFeature,
            //    defaultValue: "false",
            //    displayName: L("TestCheckFeature"),
            //    inputType: new CheckboxInputType()
            //)[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
            //{
            //    IsVisibleOnPricingTable = true,
            //    TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
            //};

            //context.Create(
            //    AppFeatures.TestCheckFeature2,
            //    defaultValue: "true",
            //    displayName: L("TestCheckFeature2"),
            //    inputType: new CheckboxInputType()
            //)[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
            //{
            //    IsVisibleOnPricingTable = true,
            //    TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
            //};

            context.Create(
             AppFeatures.Monitoring,
             defaultValue: "true",
             displayName: L("Monitoring"),
             inputType: new CheckboxInputType()
         )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
         {
             IsVisibleOnPricingTable = true,
             TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
         };
            context.Create(
            AppFeatures.Management,
            defaultValue: "true",
            displayName: L("Management"),
            inputType: new CheckboxInputType()
        )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
        {
            IsVisibleOnPricingTable = true,
            TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
        };
            context.Create(
        AppFeatures.Report,
        defaultValue: "true",
        displayName: L("Report"),
        inputType: new CheckboxInputType()
    )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
    {
        IsVisibleOnPricingTable = true,
        TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
    };
            context.Create(
   AppFeatures.Exceldownloads,
   defaultValue: "true",
   displayName: L("Exceldownloads"),
   inputType: new CheckboxInputType()
)[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
{
    IsVisibleOnPricingTable = true,
    TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
};
            context.Create(
            AppFeatures.Dashboards,
            defaultValue: "true",
            displayName: L("Dashboards"),
            inputType: new CheckboxInputType()
            )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
            {
                IsVisibleOnPricingTable = true,
                TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
            };
            context.Create(
          AppFeatures.Enduseraccess,
          defaultValue: "true",
          displayName: L("Enduseraccess"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
       AppFeatures.Rfidcardmanagement,
       defaultValue: "true",
       displayName: L("Rfidcardmanagement"),
       inputType: new CheckboxInputType()
       )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
       {
           IsVisibleOnPricingTable = true,
           TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
       };
            context.Create(
            AppFeatures.FeeSettlement,
            defaultValue: "true",
            displayName: L("FeeSettlement"),
            inputType: new CheckboxInputType()
            )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
            {
                IsVisibleOnPricingTable = true,
                TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
            };
            context.Create(
           AppFeatures.Topologysupport,
           defaultValue: "true",
           displayName: L("Topologysupport"),
           inputType: new CheckboxInputType()
           )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
           {
               IsVisibleOnPricingTable = true,
               TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
           };
            context.Create(
           AppFeatures.Paymenttypes,
           defaultValue: "true",
           displayName: L("Paymenttypes"),
           inputType: new CheckboxInputType()
           )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
           {
               IsVisibleOnPricingTable = true,
               TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
           };
            context.Create(
          AppFeatures.Subcustomersupport,
          defaultValue: "true",
          displayName: L("Subcustomersupport"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };

            context.Create(
          AppFeatures.Callcenter8x5,
          defaultValue: "true",
          displayName: L("Callcenter8x5"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.Callcenter24x7,
          defaultValue: "true",
          displayName: L("Callcenter24x7"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.Smartcharging,
          defaultValue: "true",
          displayName: L("Smartcharging"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.Phasebalancing,
          defaultValue: "true",
          displayName: L("Phasebalancing"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.SIMcards,
          defaultValue: "true",
          displayName: L("SIMcards"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.SpineBilling,
          defaultValue: "true",
          displayName: L("SpineBilling"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.ERoaming,
          defaultValue: "true",
          displayName: L("ERoaming"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.Whitelabel,
          defaultValue: "true",
          displayName: L("Whitelabel"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.Rulemanagement,
          defaultValue: "true",
          displayName: L("Rulemanagement"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.Energybilling,
          defaultValue: "true",
          displayName: L("Energybilling"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };
            context.Create(
          AppFeatures.Pricingcalculator,
          defaultValue: "true",
          displayName: L("Pricingcalculator"),
          inputType: new CheckboxInputType()
          )[FeatureMetadata.CustomFeatureKey] = new FeatureMetadata
          {
              IsVisibleOnPricingTable = true,
              TextHtmlColor = value => value == "true" ? "#5cb85c" : "#d9534f"
          };


            #endregion

#if FEATURE_SIGNALR

            var chatFeature = context.Create(
                AppFeatures.ChatFeature,
                defaultValue: "false",
                displayName: L("ChatFeature"),
                inputType: new CheckboxInputType()
            );

            chatFeature.CreateChildFeature(
                AppFeatures.TenantToTenantChatFeature,
                defaultValue: "false",
                displayName: L("TenantToTenantChatFeature"),
                inputType: new CheckboxInputType()
            );

            chatFeature.CreateChildFeature(
                AppFeatures.TenantToHostChatFeature,
                defaultValue: "false",
                displayName: L("TenantToHostChatFeature"),
                inputType: new CheckboxInputType()
            );

#endif
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, OnyxConsts.LocalizationSourceName);
        }
    }
}
