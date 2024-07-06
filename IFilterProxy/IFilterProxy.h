// 以下の ifdef ブロックは、DLL からのエクスポートを容易にするマクロを作成するための
// 一般的な方法です。この DLL 内のすべてのファイルは、コマンド ラインで定義された IFILTERPROXY_EXPORTS
// シンボルを使用してコンパイルされます。このシンボルは、この DLL を使用するプロジェクトでは定義できません。
// ソースファイルがこのファイルを含んでいる他のプロジェクトは、
// IFILTERPROXY_API 関数を DLL からインポートされたと見なすのに対し、この DLL は、このマクロで定義された
// シンボルをエクスポートされたと見なします。
#ifdef IFILTERPROXY_EXPORTS
#define IFILTERPROXY_API __declspec(dllexport)
#else
#define IFILTERPROXY_API __declspec(dllimport)
#endif

extern "C" IFILTERPROXY_API HRESULT STDAPICALLTYPE CreateIFilterProxy(
    IUnknown *pUnk,
    IFilter **ppFilter
);
