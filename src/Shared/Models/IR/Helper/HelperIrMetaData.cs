using System;
using Newtonsoft.Json;

namespace Shared.Models.IR.Helper;

public class HelperIrMetaData
{
    public static InteractionMetaData? DeserializeInteractionMetaData(string interactionMetaData)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(interactionMetaData))
            {
                InteractionMetaData metaData = JsonConvert.DeserializeObject<InteractionMetaData>(interactionMetaData);
                return metaData;
            }

        }
        catch (Exception ex)
        {

         
        }
        return null;
    }
}