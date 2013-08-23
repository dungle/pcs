using System.Collections.Generic;
using System.Linq;
using PCSComUtils.DataAccess;
using PCSComUtils.DataContext;

namespace PCSComMaterials.Inventory.BO
{
    public class InventoryUtilities
    {
        private static readonly object SyncRoot = new object();
        private static InventoryUtilities _instance;

        private InventoryUtilities()
        {
        }

        /// <summary>
        /// Gets the instance of Utilities
        /// </summary>
        /// <value>The instance.</value>
        public static InventoryUtilities Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new InventoryUtilities();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Lists the bin cache.
        /// </summary>
        /// <param name="locationId">The location id.</param>
        /// <param name="binId">The bin id.</param>
        /// <param name="productId">The product id.</param>
        /// <returns></returns>
        public List<IV_BinCache> ListBinCache(int locationId, int binId, int productId)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return locationId > 0
                       ? (binId > 0
                              ? (productId > 0
                                     ? dataContext.IV_BinCaches.Where(b => b.LocationID == locationId && b.BinID == binId && b.ProductID == productId).ToList()
                                     : dataContext.IV_BinCaches.Where(b => b.LocationID == locationId && b.BinID == binId).ToList())
                              : (productId > 0
                                     ? dataContext.IV_BinCaches.Where(b => b.LocationID == locationId && b.ProductID == productId).ToList()
                                     : dataContext.IV_BinCaches.Where(b => b.LocationID == locationId).ToList()))
                       : (productId > 0
                                     ? dataContext.IV_BinCaches.Where(b => b.ProductID == productId).ToList()
                                     : dataContext.IV_BinCaches.ToList());
        }

        /// <summary>
        ///     Get location information, including all bin information
        /// </summary>
        /// <param name="locationId">Location Id to get</param>
        /// <returns></returns>
        public MST_Location GetLocationInfo(int locationId)
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return dataContext.MST_Locations.FirstOrDefault(l => l.LocationID == locationId);
        }

        /// <summary>
        /// Gets the default counting method (Count)
        /// </summary>
        /// <returns></returns>
        public IV_CoutingMethod GetDefaultCountingMethod()
        {
            var dataContext = new PCSDataContext(Utils.Instance.ConnectionString);
            return dataContext.IV_CoutingMethods.FirstOrDefault(e => e.CountingMethodID == 1);
        }
    }
}
