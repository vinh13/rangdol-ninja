using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    public static Purchaser Instance;


    public static string removeAds = "removeAds";



    Action callAction;
    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }
    private void Start()
    {
        if (m_StoreController == null)
        {
            InitializaPurchsing();
        }
    }
    public void InitializaPurchsing()
    {
        if (IsInnitialized())
        {
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(removeAds, ProductType.NonConsumable);


        UnityPurchasing.Initialize(this, builder);
    }
    private bool IsInnitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void PruductID(string valPack, Action temp)
    {
        callAction = temp;
        BuyPruductID(valPack);
    }

    private void BuyPruductID(String productId)
    {
        if (IsInnitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }

        }
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {

        if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            callAction.Invoke();
        }
       
        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

    }
    public void RestorePurchases()
    {
        if (!IsInnitialized())
        {

            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {


            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions((result) =>
            {
                PlayerPrefs.SetInt(removeAds, 1);
                CanvasManager.Instance.removeAdsBtn.SetActive(false);


            });

        }

    }
    public void BtnRestore()
    {
        if (!IsInnitialized())
        {


            return;
        }

        Product product = m_StoreController.products.WithID(removeAds);
        if (product != null && product.hasReceipt)
        {
            PlayerPrefs.SetInt(removeAds, 1);
            CanvasManager.Instance.removeAdsBtn.SetActive(false);
        }

    }


}