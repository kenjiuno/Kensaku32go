// IFilterProxy.cpp : DLL 用にエクスポートされる関数を定義します。
//

#include "pch.h"
#include "framework.h"
#include "IFilterProxy.h"

class CMyModule : public CAtlDllModuleT<CMyModule>
{
};

CMyModule _AtlModule;

class ATL_NO_VTABLE CIFilterProxy :
    public CComObjectRootEx<CComSingleThreadModel>,
    public CComCoClass<IFilter>,
    public IFilter
{
    BEGIN_COM_MAP(CIFilterProxy)
        COM_INTERFACE_ENTRY(IFilter)
    END_COM_MAP()

public:
    CComPtr<IFilter> _filter;

    virtual ~CIFilterProxy() {}

    virtual SCODE STDMETHODCALLTYPE Init(
        /* [in] */ ULONG grfFlags,
        /* [in] */ ULONG cAttributes,
        /* [size_is][in] */ const FULLPROPSPEC* aAttributes,
        /* [out] */ ULONG* pFlags) {
        return _filter->Init(grfFlags, cAttributes, aAttributes, pFlags);
    }

    virtual SCODE STDMETHODCALLTYPE GetChunk(
        /* [out] */ STAT_CHUNK* pStat) {
        return _filter->GetChunk(pStat);
    }

    virtual SCODE STDMETHODCALLTYPE GetText(
        /* [out][in] */ ULONG* pcwcBuffer,
        /* [size_is][out] */ WCHAR* awcBuffer) {
        return _filter->GetText(pcwcBuffer, awcBuffer);
    }

    virtual SCODE STDMETHODCALLTYPE GetValue(
        /* [out] */ PROPVARIANT** ppPropValue) {
        return _filter->GetValue(ppPropValue);
    }

    virtual SCODE STDMETHODCALLTYPE BindRegion(
        /* [in] */ FILTERREGION origPos,
        /* [in] */ REFIID riid,
        /* [out] */ void** ppunk) {
        return _filter->BindRegion(origPos, riid, ppunk);
    }
};

HRESULT STDAPICALLTYPE CreateIFilterProxy(
    IUnknown* pUnk,
    IFilter** ppFilter
)
{
    CComQIPtr<IFilter> sourceFilter = pUnk;
    if (sourceFilter == NULL) {
        *ppFilter = NULL;
        return E_NOINTERFACE;
    }
    else {
        CComObject<CIFilterProxy>* pp = nullptr;
        HRESULT hr;
        if (FAILED(hr = CComObject<CIFilterProxy>::CreateInstance(&pp)) || pp == NULL) {
            return hr;
        }
        else {
            pp->AddRef();
            pp->_filter = sourceFilter;
            *ppFilter = pp;
            return S_OK;
        }
    }
}
