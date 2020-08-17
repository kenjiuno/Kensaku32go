namespace Kensaku32go.Utils
{
    public class Strut
    {
        public static int Next(string s, ref int x, int maxx)
        {
            if (x + 0 < maxx)
            {
                switch ((int)s[x + 0])
                {
                    case 0x0021: // !
                        x += 1;
                        return 0x0021; // !
                    case 0x0022: // "
                        x += 1;
                        return 0x201D; // ”
                    case 0x0027: // '
                        x += 1;
                        return 0x201D; // ”
                    case 0x0028: // (
                        x += 1;
                        return 0x003C; // <
                    case 0x0029: // )
                        x += 1;
                        return 0x003E; // >
                    case 0x002C: // ,
                        x += 1;
                        return 0x002C; // ,
                    case 0x002E: // .
                        x += 1;
                        return 0x002E; // .
                    case 0x003C: // <
                        x += 1;
                        return 0x003C; // <
                    case 0x003E: // >
                        x += 1;
                        return 0x003E; // >
                    case 0x003F: // ?
                        x += 1;
                        return 0x003F; // ?
                    case 0x0041: // A
                        x += 1;
                        return 0x0061; // a
                    case 0x0042: // B
                        x += 1;
                        return 0x0062; // b
                    case 0x0043: // C
                        x += 1;
                        return 0x0063; // c
                    case 0x0044: // D
                        x += 1;
                        return 0x0064; // d
                    case 0x0045: // E
                        x += 1;
                        return 0x0065; // e
                    case 0x0046: // F
                        x += 1;
                        return 0x0066; // f
                    case 0x0047: // G
                        x += 1;
                        return 0x0067; // g
                    case 0x0048: // H
                        x += 1;
                        return 0x0068; // h
                    case 0x0049: // I
                        x += 1;
                        return 0x0069; // i
                    case 0x004A: // J
                        x += 1;
                        return 0x006A; // j
                    case 0x004B: // K
                        x += 1;
                        return 0x006B; // k
                    case 0x004C: // L
                        x += 1;
                        return 0x006C; // l
                    case 0x004D: // M
                        x += 1;
                        return 0x006D; // m
                    case 0x004E: // N
                        x += 1;
                        return 0x006E; // n
                    case 0x004F: // O
                        x += 1;
                        return 0x006F; // o
                    case 0x0050: // P
                        x += 1;
                        return 0x0070; // p
                    case 0x0051: // Q
                        x += 1;
                        return 0x0071; // q
                    case 0x0052: // R
                        x += 1;
                        return 0x0072; // r
                    case 0x0053: // S
                        x += 1;
                        return 0x0073; // s
                    case 0x0054: // T
                        x += 1;
                        return 0x0074; // t
                    case 0x0055: // U
                        x += 1;
                        return 0x0075; // u
                    case 0x0056: // V
                        x += 1;
                        return 0x0076; // v
                    case 0x0057: // W
                        x += 1;
                        return 0x0077; // w
                    case 0x0058: // X
                        x += 1;
                        return 0x0078; // x
                    case 0x0059: // Y
                        x += 1;
                        return 0x0079; // y
                    case 0x005A: // Z
                        x += 1;
                        return 0x007A; // z
                    case 0x005B: // [
                        x += 1;
                        return 0x003C; // <
                    case 0x005D: // ]
                        x += 1;
                        return 0x003E; // >
                    case 0x0060: // `
                        x += 1;
                        return 0x201D; // ”
                    case 0x0061: // a
                        x += 1;
                        return 0x0061; // a
                    case 0x0062: // b
                        x += 1;
                        return 0x0062; // b
                    case 0x0063: // c
                        x += 1;
                        return 0x0063; // c
                    case 0x0064: // d
                        x += 1;
                        return 0x0064; // d
                    case 0x0065: // e
                        x += 1;
                        return 0x0065; // e
                    case 0x0066: // f
                        x += 1;
                        return 0x0066; // f
                    case 0x0067: // g
                        x += 1;
                        return 0x0067; // g
                    case 0x0068: // h
                        x += 1;
                        return 0x0068; // h
                    case 0x0069: // i
                        x += 1;
                        return 0x0069; // i
                    case 0x006A: // j
                        x += 1;
                        return 0x006A; // j
                    case 0x006B: // k
                        x += 1;
                        return 0x006B; // k
                    case 0x006C: // l
                        x += 1;
                        return 0x006C; // l
                    case 0x006D: // m
                        x += 1;
                        return 0x006D; // m
                    case 0x006E: // n
                        x += 1;
                        return 0x006E; // n
                    case 0x006F: // o
                        x += 1;
                        return 0x006F; // o
                    case 0x0070: // p
                        x += 1;
                        return 0x0070; // p
                    case 0x0071: // q
                        x += 1;
                        return 0x0071; // q
                    case 0x0072: // r
                        x += 1;
                        return 0x0072; // r
                    case 0x0073: // s
                        x += 1;
                        return 0x0073; // s
                    case 0x0074: // t
                        x += 1;
                        return 0x0074; // t
                    case 0x0075: // u
                        x += 1;
                        return 0x0075; // u
                    case 0x0076: // v
                        x += 1;
                        return 0x0076; // v
                    case 0x0077: // w
                        x += 1;
                        return 0x0077; // w
                    case 0x0078: // x
                        x += 1;
                        return 0x0078; // x
                    case 0x0079: // y
                        x += 1;
                        return 0x0079; // y
                    case 0x007A: // z
                        x += 1;
                        return 0x007A; // z
                    case 0x007B: // {
                        x += 1;
                        return 0x003C; // <
                    case 0x007D: // }
                        x += 1;
                        return 0x003E; // >
                    case 0x2018: // ‘
                        x += 1;
                        return 0x201D; // ”
                    case 0x2019: // ’
                        x += 1;
                        return 0x201D; // ”
                    case 0x201D: // ”
                        x += 1;
                        return 0x201D; // ”
                    case 0x3001: // 、
                        x += 1;
                        return 0x002C; // ,
                    case 0x3002: // 。
                        x += 1;
                        return 0x002E; // .
                    case 0x300C: // 「
                        x += 1;
                        return 0x003C; // <
                    case 0x300D: // 」
                        x += 1;
                        return 0x003E; // >
                    case 0x300E: // 『
                        x += 1;
                        return 0x003C; // <
                    case 0x300F: // 』
                        x += 1;
                        return 0x003E; // >
                    case 0x3010: // 【
                        x += 1;
                        return 0x003C; // <
                    case 0x3011: // 】
                        x += 1;
                        return 0x003E; // >
                    case 0x3041: // ぁ
                        x += 1;
                        return 0x3042; // あ
                    case 0x3042: // あ
                        x += 1;
                        return 0x3042; // あ
                    case 0x3043: // ぃ
                        x += 1;
                        return 0x3044; // い
                    case 0x3044: // い
                        x += 1;
                        return 0x3044; // い
                    case 0x3045: // ぅ
                        x += 1;
                        return 0x3046; // う
                    case 0x3046: // う
                        x += 1;
                        return 0x3046; // う
                    case 0x3047: // ぇ
                        x += 1;
                        return 0x3048; // え
                    case 0x3048: // え
                        x += 1;
                        return 0x3048; // え
                    case 0x3049: // ぉ
                        x += 1;
                        return 0x304A; // お
                    case 0x304A: // お
                        x += 1;
                        return 0x304A; // お
                    case 0x304B: // か
                        x += 1;
                        return 0x304B; // か
                    case 0x304C: // が
                        x += 1;
                        return 0x304C; // が
                    case 0x304D: // き
                        x += 1;
                        return 0x304D; // き
                    case 0x304E: // ぎ
                        x += 1;
                        return 0x304E; // ぎ
                    case 0x304F: // く
                        x += 1;
                        return 0x304F; // く
                    case 0x3050: // ぐ
                        x += 1;
                        return 0x3050; // ぐ
                    case 0x3051: // け
                        x += 1;
                        return 0x3051; // け
                    case 0x3052: // げ
                        x += 1;
                        return 0x3052; // げ
                    case 0x3053: // こ
                        x += 1;
                        return 0x3053; // こ
                    case 0x3054: // ご
                        x += 1;
                        return 0x3054; // ご
                    case 0x3055: // さ
                        x += 1;
                        return 0x3055; // さ
                    case 0x3056: // ざ
                        x += 1;
                        return 0x3056; // ざ
                    case 0x3057: // し
                        x += 1;
                        return 0x3057; // し
                    case 0x3058: // じ
                        x += 1;
                        return 0x3058; // じ
                    case 0x3059: // す
                        x += 1;
                        return 0x3059; // す
                    case 0x305A: // ず
                        x += 1;
                        return 0x305A; // ず
                    case 0x305B: // せ
                        x += 1;
                        return 0x305B; // せ
                    case 0x305C: // ぜ
                        x += 1;
                        return 0x305C; // ぜ
                    case 0x305D: // そ
                        x += 1;
                        return 0x305D; // そ
                    case 0x305E: // ぞ
                        x += 1;
                        return 0x305E; // ぞ
                    case 0x305F: // た
                        x += 1;
                        return 0x305F; // た
                    case 0x3060: // だ
                        x += 1;
                        return 0x3060; // だ
                    case 0x3061: // ち
                        x += 1;
                        return 0x3061; // ち
                    case 0x3062: // ぢ
                        x += 1;
                        return 0x3062; // ぢ
                    case 0x3063: // っ
                        x += 1;
                        return 0x3064; // つ
                    case 0x3064: // つ
                        x += 1;
                        return 0x3064; // つ
                    case 0x3065: // づ
                        x += 1;
                        return 0x3065; // づ
                    case 0x3066: // て
                        x += 1;
                        return 0x3066; // て
                    case 0x3067: // で
                        x += 1;
                        return 0x3067; // で
                    case 0x3068: // と
                        x += 1;
                        return 0x3068; // と
                    case 0x3069: // ど
                        x += 1;
                        return 0x3069; // ど
                    case 0x306A: // な
                        x += 1;
                        return 0x306A; // な
                    case 0x306B: // に
                        x += 1;
                        return 0x306B; // に
                    case 0x306C: // ぬ
                        x += 1;
                        return 0x306C; // ぬ
                    case 0x306D: // ね
                        x += 1;
                        return 0x306D; // ね
                    case 0x306E: // の
                        x += 1;
                        return 0x306E; // の
                    case 0x306F: // は
                        x += 1;
                        return 0x306F; // は
                    case 0x3070: // ば
                        x += 1;
                        return 0x3070; // ば
                    case 0x3071: // ぱ
                        x += 1;
                        return 0x3071; // ぱ
                    case 0x3072: // ひ
                        x += 1;
                        return 0x3072; // ひ
                    case 0x3073: // び
                        x += 1;
                        return 0x3073; // び
                    case 0x3074: // ぴ
                        x += 1;
                        return 0x3074; // ぴ
                    case 0x3075: // ふ
                        x += 1;
                        return 0x3075; // ふ
                    case 0x3076: // ぶ
                        x += 1;
                        return 0x3076; // ぶ
                    case 0x3077: // ぷ
                        x += 1;
                        return 0x3077; // ぷ
                    case 0x3078: // へ
                        x += 1;
                        return 0x3078; // へ
                    case 0x3079: // べ
                        x += 1;
                        return 0x3079; // べ
                    case 0x307A: // ぺ
                        x += 1;
                        return 0x307A; // ぺ
                    case 0x307B: // ほ
                        x += 1;
                        return 0x307B; // ほ
                    case 0x307C: // ぼ
                        x += 1;
                        return 0x307C; // ぼ
                    case 0x307D: // ぽ
                        x += 1;
                        return 0x307D; // ぽ
                    case 0x307E: // ま
                        x += 1;
                        return 0x307E; // ま
                    case 0x307F: // み
                        x += 1;
                        return 0x307F; // み
                    case 0x3080: // む
                        x += 1;
                        return 0x3080; // む
                    case 0x3081: // め
                        x += 1;
                        return 0x3081; // め
                    case 0x3082: // も
                        x += 1;
                        return 0x3082; // も
                    case 0x3083: // ゃ
                        x += 1;
                        return 0x3084; // や
                    case 0x3084: // や
                        x += 1;
                        return 0x3084; // や
                    case 0x3085: // ゅ
                        x += 1;
                        return 0x3086; // ゆ
                    case 0x3086: // ゆ
                        x += 1;
                        return 0x3086; // ゆ
                    case 0x3087: // ょ
                        x += 1;
                        return 0x3088; // よ
                    case 0x3088: // よ
                        x += 1;
                        return 0x3088; // よ
                    case 0x3089: // ら
                        x += 1;
                        return 0x3089; // ら
                    case 0x308A: // り
                        x += 1;
                        return 0x308A; // り
                    case 0x308B: // る
                        x += 1;
                        return 0x308B; // る
                    case 0x308C: // れ
                        x += 1;
                        return 0x308C; // れ
                    case 0x308D: // ろ
                        x += 1;
                        return 0x308D; // ろ
                    case 0x308F: // わ
                        x += 1;
                        return 0x308F; // わ
                    case 0x3090: // ゐ
                        x += 1;
                        return 0x3044; // い
                    case 0x3091: // ゑ
                        x += 1;
                        return 0x3048; // え
                    case 0x3092: // を
                        x += 1;
                        return 0x3092; // を
                    case 0x3093: // ん
                        x += 1;
                        return 0x3093; // ん
                    case 0x30A1: // ァ
                        x += 1;
                        return 0x3042; // あ
                    case 0x30A2: // ア
                        x += 1;
                        return 0x3042; // あ
                    case 0x30A3: // ィ
                        x += 1;
                        return 0x3044; // い
                    case 0x30A4: // イ
                        x += 1;
                        return 0x3044; // い
                    case 0x30A5: // ゥ
                        x += 1;
                        return 0x3046; // う
                    case 0x30A6: // ウ
                        x += 1;
                        return 0x3046; // う
                    case 0x30A7: // ェ
                        x += 1;
                        return 0x3048; // え
                    case 0x30A8: // エ
                        x += 1;
                        return 0x3048; // え
                    case 0x30A9: // ォ
                        x += 1;
                        return 0x304A; // お
                    case 0x30AA: // オ
                        x += 1;
                        return 0x304A; // お
                    case 0x30AB: // カ
                        x += 1;
                        return 0x304B; // か
                    case 0x30AC: // ガ
                        x += 1;
                        return 0x304C; // が
                    case 0x30AD: // キ
                        x += 1;
                        return 0x304D; // き
                    case 0x30AE: // ギ
                        x += 1;
                        return 0x304E; // ぎ
                    case 0x30AF: // ク
                        x += 1;
                        return 0x304F; // く
                    case 0x30B0: // グ
                        x += 1;
                        return 0x3050; // ぐ
                    case 0x30B1: // ケ
                        x += 1;
                        return 0x3051; // け
                    case 0x30B2: // ゲ
                        x += 1;
                        return 0x3052; // げ
                    case 0x30B3: // コ
                        x += 1;
                        return 0x3053; // こ
                    case 0x30B4: // ゴ
                        x += 1;
                        return 0x3054; // ご
                    case 0x30B5: // サ
                        x += 1;
                        return 0x3055; // さ
                    case 0x30B6: // ザ
                        x += 1;
                        return 0x3056; // ざ
                    case 0x30B7: // シ
                        x += 1;
                        return 0x3057; // し
                    case 0x30B8: // ジ
                        x += 1;
                        return 0x3058; // じ
                    case 0x30B9: // ス
                        x += 1;
                        return 0x3059; // す
                    case 0x30BA: // ズ
                        x += 1;
                        return 0x305A; // ず
                    case 0x30BB: // セ
                        x += 1;
                        return 0x305B; // せ
                    case 0x30BC: // ゼ
                        x += 1;
                        return 0x305C; // ぜ
                    case 0x30BD: // ソ
                        x += 1;
                        return 0x305D; // そ
                    case 0x30BE: // ゾ
                        x += 1;
                        return 0x305E; // ぞ
                    case 0x30BF: // タ
                        x += 1;
                        return 0x305F; // た
                    case 0x30C0: // ダ
                        x += 1;
                        return 0x3060; // だ
                    case 0x30C1: // チ
                        x += 1;
                        return 0x3061; // ち
                    case 0x30C2: // ヂ
                        x += 1;
                        return 0x3062; // ぢ
                    case 0x30C3: // ッ
                        x += 1;
                        return 0x3064; // つ
                    case 0x30C4: // ツ
                        x += 1;
                        return 0x3064; // つ
                    case 0x30C5: // ヅ
                        x += 1;
                        return 0x3065; // づ
                    case 0x30C6: // テ
                        x += 1;
                        return 0x3066; // て
                    case 0x30C7: // デ
                        x += 1;
                        return 0x3067; // で
                    case 0x30C8: // ト
                        x += 1;
                        return 0x3068; // と
                    case 0x30C9: // ド
                        x += 1;
                        return 0x3069; // ど
                    case 0x30CA: // ナ
                        x += 1;
                        return 0x306A; // な
                    case 0x30CB: // ニ
                        x += 1;
                        return 0x306B; // に
                    case 0x30CC: // ヌ
                        x += 1;
                        return 0x306C; // ぬ
                    case 0x30CD: // ネ
                        x += 1;
                        return 0x306D; // ね
                    case 0x30CE: // ノ
                        x += 1;
                        return 0x306E; // の
                    case 0x30CF: // ハ
                        x += 1;
                        return 0x306F; // は
                    case 0x30D0: // バ
                        x += 1;
                        return 0x3070; // ば
                    case 0x30D1: // パ
                        x += 1;
                        return 0x3071; // ぱ
                    case 0x30D2: // ヒ
                        x += 1;
                        return 0x3072; // ひ
                    case 0x30D3: // ビ
                        x += 1;
                        return 0x3073; // び
                    case 0x30D4: // ピ
                        x += 1;
                        return 0x3074; // ぴ
                    case 0x30D5: // フ
                        x += 1;
                        return 0x3075; // ふ
                    case 0x30D6: // ブ
                        x += 1;
                        return 0x3076; // ぶ
                    case 0x30D7: // プ
                        x += 1;
                        return 0x3077; // ぷ
                    case 0x30D8: // ヘ
                        x += 1;
                        return 0x3078; // へ
                    case 0x30D9: // ベ
                        x += 1;
                        return 0x3079; // べ
                    case 0x30DA: // ペ
                        x += 1;
                        return 0x307A; // ぺ
                    case 0x30DB: // ホ
                        x += 1;
                        return 0x307B; // ほ
                    case 0x30DC: // ボ
                        x += 1;
                        return 0x307C; // ぼ
                    case 0x30DD: // ポ
                        x += 1;
                        return 0x307D; // ぽ
                    case 0x30DE: // マ
                        x += 1;
                        return 0x307E; // ま
                    case 0x30DF: // ミ
                        x += 1;
                        return 0x307F; // み
                    case 0x30E0: // ム
                        x += 1;
                        return 0x3080; // む
                    case 0x30E1: // メ
                        x += 1;
                        return 0x3081; // め
                    case 0x30E2: // モ
                        x += 1;
                        return 0x3082; // も
                    case 0x30E3: // ャ
                        x += 1;
                        return 0x3084; // や
                    case 0x30E4: // ヤ
                        x += 1;
                        return 0x3084; // や
                    case 0x30E5: // ュ
                        x += 1;
                        return 0x3086; // ゆ
                    case 0x30E6: // ユ
                        x += 1;
                        return 0x3086; // ゆ
                    case 0x30E7: // ョ
                        x += 1;
                        return 0x3088; // よ
                    case 0x30E8: // ヨ
                        x += 1;
                        return 0x3088; // よ
                    case 0x30E9: // ラ
                        x += 1;
                        return 0x3089; // ら
                    case 0x30EA: // リ
                        x += 1;
                        return 0x308A; // り
                    case 0x30EB: // ル
                        x += 1;
                        return 0x308B; // る
                    case 0x30EC: // レ
                        x += 1;
                        return 0x308C; // れ
                    case 0x30ED: // ロ
                        x += 1;
                        return 0x308D; // ろ
                    case 0x30EF: // ワ
                        x += 1;
                        return 0x308F; // わ
                    case 0x30F0: // ヰ
                        x += 1;
                        return 0x3044; // い
                    case 0x30F1: // ヱ
                        x += 1;
                        return 0x3048; // え
                    case 0x30F2: // ヲ
                        x += 1;
                        return 0x3092; // を
                    case 0x30F3: // ン
                        x += 1;
                        return 0x3093; // ん
                    case 0xFF01: // ！
                        x += 1;
                        return 0x0021; // !
                    case 0xFF08: // （
                        x += 1;
                        return 0x003C; // <
                    case 0xFF09: // ）
                        x += 1;
                        return 0x003E; // >
                    case 0xFF0C: // ，
                        x += 1;
                        return 0x002C; // ,
                    case 0xFF0E: // ．
                        x += 1;
                        return 0x002E; // .
                    case 0xFF1C: // ＜
                        x += 1;
                        return 0x003C; // <
                    case 0xFF1E: // ＞
                        x += 1;
                        return 0x003E; // >
                    case 0xFF1F: // ？
                        x += 1;
                        return 0x003F; // ?
                    case 0xFF21: // Ａ
                        x += 1;
                        return 0x0061; // a
                    case 0xFF22: // Ｂ
                        x += 1;
                        return 0x0062; // b
                    case 0xFF23: // Ｃ
                        x += 1;
                        return 0x0063; // c
                    case 0xFF24: // Ｄ
                        x += 1;
                        return 0x0064; // d
                    case 0xFF25: // Ｅ
                        x += 1;
                        return 0x0065; // e
                    case 0xFF26: // Ｆ
                        x += 1;
                        return 0x0066; // f
                    case 0xFF27: // Ｇ
                        x += 1;
                        return 0x0067; // g
                    case 0xFF28: // Ｈ
                        x += 1;
                        return 0x0068; // h
                    case 0xFF29: // Ｉ
                        x += 1;
                        return 0x0069; // i
                    case 0xFF2A: // Ｊ
                        x += 1;
                        return 0x006A; // j
                    case 0xFF2B: // Ｋ
                        x += 1;
                        return 0x006B; // k
                    case 0xFF2C: // Ｌ
                        x += 1;
                        return 0x006C; // l
                    case 0xFF2D: // Ｍ
                        x += 1;
                        return 0x006D; // m
                    case 0xFF2E: // Ｎ
                        x += 1;
                        return 0x006E; // n
                    case 0xFF2F: // Ｏ
                        x += 1;
                        return 0x006F; // o
                    case 0xFF30: // Ｐ
                        x += 1;
                        return 0x0070; // p
                    case 0xFF31: // Ｑ
                        x += 1;
                        return 0x0071; // q
                    case 0xFF32: // Ｒ
                        x += 1;
                        return 0x0072; // r
                    case 0xFF33: // Ｓ
                        x += 1;
                        return 0x0073; // s
                    case 0xFF34: // Ｔ
                        x += 1;
                        return 0x0074; // t
                    case 0xFF35: // Ｕ
                        x += 1;
                        return 0x0075; // u
                    case 0xFF36: // Ｖ
                        x += 1;
                        return 0x0076; // v
                    case 0xFF37: // Ｗ
                        x += 1;
                        return 0x0077; // w
                    case 0xFF38: // Ｘ
                        x += 1;
                        return 0x0078; // x
                    case 0xFF39: // Ｙ
                        x += 1;
                        return 0x0079; // y
                    case 0xFF3A: // Ｚ
                        x += 1;
                        return 0x007A; // z
                    case 0xFF3B: // ［
                        x += 1;
                        return 0x003C; // <
                    case 0xFF3D: // ］
                        x += 1;
                        return 0x003E; // >
                    case 0xFF41: // ａ
                        x += 1;
                        return 0x0061; // a
                    case 0xFF42: // ｂ
                        x += 1;
                        return 0x0062; // b
                    case 0xFF43: // ｃ
                        x += 1;
                        return 0x0063; // c
                    case 0xFF44: // ｄ
                        x += 1;
                        return 0x0064; // d
                    case 0xFF45: // ｅ
                        x += 1;
                        return 0x0065; // e
                    case 0xFF46: // ｆ
                        x += 1;
                        return 0x0066; // f
                    case 0xFF47: // ｇ
                        x += 1;
                        return 0x0067; // g
                    case 0xFF48: // ｈ
                        x += 1;
                        return 0x0068; // h
                    case 0xFF49: // ｉ
                        x += 1;
                        return 0x0069; // i
                    case 0xFF4A: // ｊ
                        x += 1;
                        return 0x006A; // j
                    case 0xFF4B: // ｋ
                        x += 1;
                        return 0x006B; // k
                    case 0xFF4C: // ｌ
                        x += 1;
                        return 0x006C; // l
                    case 0xFF4D: // ｍ
                        x += 1;
                        return 0x006D; // m
                    case 0xFF4E: // ｎ
                        x += 1;
                        return 0x006E; // n
                    case 0xFF4F: // ｏ
                        x += 1;
                        return 0x006F; // o
                    case 0xFF50: // ｐ
                        x += 1;
                        return 0x0070; // p
                    case 0xFF51: // ｑ
                        x += 1;
                        return 0x0071; // q
                    case 0xFF52: // ｒ
                        x += 1;
                        return 0x0072; // r
                    case 0xFF53: // ｓ
                        x += 1;
                        return 0x0073; // s
                    case 0xFF54: // ｔ
                        x += 1;
                        return 0x0074; // t
                    case 0xFF55: // ｕ
                        x += 1;
                        return 0x0075; // u
                    case 0xFF56: // ｖ
                        x += 1;
                        return 0x0076; // v
                    case 0xFF57: // ｗ
                        x += 1;
                        return 0x0077; // w
                    case 0xFF58: // ｘ
                        x += 1;
                        return 0x0078; // x
                    case 0xFF59: // ｙ
                        x += 1;
                        return 0x0079; // y
                    case 0xFF5A: // ｚ
                        x += 1;
                        return 0x007A; // z
                    case 0xFF5B: // ｛
                        x += 1;
                        return 0x003C; // <
                    case 0xFF5D: // ｝
                        x += 1;
                        return 0x003E; // >
                    case 0xFF61: // ｡
                        x += 1;
                        return 0x002E; // .
                    case 0xFF64: // ､
                        x += 1;
                        return 0x002C; // ,
                    case 0xFF66: // ｦ
                        x += 1;
                        return 0x3092; // を
                    case 0xFF67: // ｧ
                        x += 1;
                        return 0x3042; // あ
                    case 0xFF68: // ｨ
                        x += 1;
                        return 0x3044; // い
                    case 0xFF69: // ｩ
                        x += 1;
                        return 0x3046; // う
                    case 0xFF6A: // ｪ
                        x += 1;
                        return 0x3048; // え
                    case 0xFF6B: // ｫ
                        x += 1;
                        return 0x304A; // お
                    case 0xFF6C: // ｬ
                        x += 1;
                        return 0x3084; // や
                    case 0xFF6D: // ｭ
                        x += 1;
                        return 0x3086; // ゆ
                    case 0xFF6E: // ｮ
                        x += 1;
                        return 0x3088; // よ
                    case 0xFF6F: // ｯ
                        x += 1;
                        return 0x3064; // つ
                    case 0xFF71: // ｱ
                        x += 1;
                        return 0x3042; // あ
                    case 0xFF72: // ｲ
                        x += 1;
                        return 0x3044; // い
                    case 0xFF73: // ｳ
                        x += 1;
                        return 0x3046; // う
                    case 0xFF74: // ｴ
                        x += 1;
                        return 0x3048; // え
                    case 0xFF75: // ｵ
                        x += 1;
                        return 0x304A; // お
                    case 0xFF76: // ｶ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x304C; // が
                            }
                        }
                        x += 1;
                        return 0x304B; // か
                    case 0xFF77: // ｷ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x304E; // ぎ
                            }
                        }
                        x += 1;
                        return 0x304D; // き
                    case 0xFF78: // ｸ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3050; // ぐ
                            }
                        }
                        x += 1;
                        return 0x304F; // く
                    case 0xFF79: // ｹ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3052; // げ
                            }
                        }
                        x += 1;
                        return 0x3051; // け
                    case 0xFF7A: // ｺ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3054; // ご
                            }
                        }
                        x += 1;
                        return 0x3053; // こ
                    case 0xFF7B: // ｻ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3056; // ざ
                            }
                        }
                        x += 1;
                        return 0x3055; // さ
                    case 0xFF7C: // ｼ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3058; // じ
                            }
                        }
                        x += 1;
                        return 0x3057; // し
                    case 0xFF7D: // ｽ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x305A; // ず
                            }
                        }
                        x += 1;
                        return 0x3059; // す
                    case 0xFF7E: // ｾ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x305C; // ぜ
                            }
                        }
                        x += 1;
                        return 0x305B; // せ
                    case 0xFF7F: // ｿ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x305E; // ぞ
                            }
                        }
                        x += 1;
                        return 0x305D; // そ
                    case 0xFF80: // ﾀ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3060; // だ
                            }
                        }
                        x += 1;
                        return 0x305F; // た
                    case 0xFF81: // ﾁ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3062; // ぢ
                            }
                        }
                        x += 1;
                        return 0x3061; // ち
                    case 0xFF82: // ﾂ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3065; // づ
                            }
                        }
                        x += 1;
                        return 0x3064; // つ
                    case 0xFF83: // ﾃ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3067; // で
                            }
                        }
                        x += 1;
                        return 0x3066; // て
                    case 0xFF84: // ﾄ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3069; // ど
                            }
                        }
                        x += 1;
                        return 0x3068; // と
                    case 0xFF85: // ﾅ
                        x += 1;
                        return 0x306A; // な
                    case 0xFF86: // ﾆ
                        x += 1;
                        return 0x306B; // に
                    case 0xFF87: // ﾇ
                        x += 1;
                        return 0x306C; // ぬ
                    case 0xFF88: // ﾈ
                        x += 1;
                        return 0x306D; // ね
                    case 0xFF89: // ﾉ
                        x += 1;
                        return 0x306E; // の
                    case 0xFF8A: // ﾊ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3070; // ば
                                case 0xFF9F: // ﾟ
                                    x += 2;
                                    return 0x3071; // ぱ
                            }
                        }
                        x += 1;
                        return 0x306F; // は
                    case 0xFF8B: // ﾋ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3073; // び
                                case 0xFF9F: // ﾟ
                                    x += 2;
                                    return 0x3074; // ぴ
                            }
                        }
                        x += 1;
                        return 0x3072; // ひ
                    case 0xFF8C: // ﾌ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3076; // ぶ
                                case 0xFF9F: // ﾟ
                                    x += 2;
                                    return 0x3077; // ぷ
                            }
                        }
                        x += 1;
                        return 0x3075; // ふ
                    case 0xFF8D: // ﾍ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x3079; // べ
                                case 0xFF9F: // ﾟ
                                    x += 2;
                                    return 0x307A; // ぺ
                            }
                        }
                        x += 1;
                        return 0x3078; // へ
                    case 0xFF8E: // ﾎ
                        if (x + 1 < maxx)
                        {
                            switch ((int)s[x + 1])
                            {
                                case 0xFF9E: // ﾞ
                                    x += 2;
                                    return 0x307C; // ぼ
                                case 0xFF9F: // ﾟ
                                    x += 2;
                                    return 0x307D; // ぽ
                            }
                        }
                        x += 1;
                        return 0x307B; // ほ
                    case 0xFF8F: // ﾏ
                        x += 1;
                        return 0x307E; // ま
                    case 0xFF90: // ﾐ
                        x += 1;
                        return 0x307F; // み
                    case 0xFF91: // ﾑ
                        x += 1;
                        return 0x3080; // む
                    case 0xFF92: // ﾒ
                        x += 1;
                        return 0x3081; // め
                    case 0xFF93: // ﾓ
                        x += 1;
                        return 0x3082; // も
                    case 0xFF94: // ﾔ
                        x += 1;
                        return 0x3084; // や
                    case 0xFF95: // ﾕ
                        x += 1;
                        return 0x3086; // ゆ
                    case 0xFF96: // ﾖ
                        x += 1;
                        return 0x3088; // よ
                    case 0xFF97: // ﾗ
                        x += 1;
                        return 0x3089; // ら
                    case 0xFF98: // ﾘ
                        x += 1;
                        return 0x308A; // り
                    case 0xFF99: // ﾙ
                        x += 1;
                        return 0x308B; // る
                    case 0xFF9A: // ﾚ
                        x += 1;
                        return 0x308C; // れ
                    case 0xFF9B: // ﾛ
                        x += 1;
                        return 0x308D; // ろ
                    case 0xFF9C: // ﾜ
                        x += 1;
                        return 0x308F; // わ
                    case 0xFF9D: // ﾝ
                        x += 1;
                        return 0x3093; // ん
                }
            }

            if (x < maxx)
            {
                char c = s[x];
                x += 1;
                return c;
            }
            return -1;
        }
    }
}