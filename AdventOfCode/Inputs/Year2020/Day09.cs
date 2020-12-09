﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Inputs.Year2020
{
    public class Day09
    {
        public string Input => @"1
15
32
16
6
20
25
30
38
31
48
47
19
23
39
50
41
4
27
12
21
24
26
43
2
3
5
7
8
9
6
72
10
20
11
13
14
30
25
23
15
16
17
18
19
22
21
12
32
24
28
26
27
35
29
31
33
34
36
37
38
42
39
30
40
43
41
47
46
44
50
49
45
75
51
53
57
56
81
59
83
70
64
72
67
91
69
71
73
84
85
92
89
90
93
96
94
98
123
104
120
154
115
126
141
133
131
157
139
287
238
209
144
158
175
185
179
182
183
187
190
192
286
289
264
257
241
256
259
466
270
275
283
324
462
302
323
331
333
362
361
372
365
370
377
524
475
497
498
521
844
500
515
529
545
553
558
585
1013
887
1210
654
995
694
735
876
872
894
747
906
1060
972
1607
998
1268
1247
1292
1199
1074
1239
1111
1768
1696
1348
1389
1401
1429
1611
1441
1870
1619
1641
1653
1745
1878
1970
3279
2072
5024
2273
2185
2310
2313
3424
3307
2459
3052
3349
2737
2790
4155
4732
4720
3311
3623
4898
6214
3398
3715
5534
4042
4257
4345
4458
4495
7033
6618
4772
5196
8673
6994
5527
8940
9525
6832
6934
8838
10016
6709
7021
7113
7440
14107
11805
9238
16380
8602
9230
8953
9267
13639
14200
11706
15536
13541
15961
13766
21313
13643
15859
13730
16678
13822
14134
14461
16042
16393
17555
17832
17840
17869
18183
29175
30215
25126
45853
25247
25349
27184
27271
27373
33414
27777
27465
27552
27864
34576
32330
28595
30503
43079
35395
35387
35672
60798
58540
43309
50373
57588
50475
50596
52518
55017
54455
54838
54925
58280
55242
70943
55416
56459
125868
59098
85983
73582
70782
71059
85862
98234
100969
172028
109293
105891
101071
105434
159351
125514
109380
109697
109763
176673
251382
156765
111875
115557
144364
144960
129880
141841
144641
270262
223748
184096
199203
202040
211325
206505
250255
210768
274244
219077
219143
253744
340648
221638
355132
227432
286482
271721
245437
348957
274840
366279
449458
721179
383299
386136
394864
996019
408545
417273
605387
429845
440715
505625
464580
627130
576770
449070
467075
472869
499153
517158
520277
735093
623797
660976
885289
778163
791844
769435
781000
803409
849260
825818
990027
870560
947003
948223
931655
966228
916145
1025840
1404797
1090872
972022
1486794
1289712
2351800
1464385
1603981
1430411
1547598
2396858
1550435
1673969
3641512
1974063
2115530
1956255
1920245
2495821
2498658
1847800
1882373
2007017
1888167
3735967
2261734
3963043
2402433
2720123
4199600
2894796
2980846
3098033
4217989
3556342
4452076
3224404
3562136
3770540
3730173
3895184
3768045
3802618
4290600
3854817
3889390
5992829
8306893
5690785
8712952
7692008
5122556
6748891
6450296
5875642
6078879
9846924
8346960
9554965
16595815
6786540
6954577
9458830
7498218
7532791
13567650
7570663
7657435
7744207
12529175
9011946
10813341
12077133
14134502
14525240
12865419
14698784
11954521
12325938
12662182
13033456
22890474
13741117
20406389
26211635
14284758
14452795
21951013
15031009
15103454
18384004
15228098
15401642
16756153
27486251
15690279
24988120
27896428
52474371
24280459
30918377
24616703
24987977
25359394
25695638
38392850
28025875
42115547
30134463
39644152
29512856
29483804
30259107
30505096
30629740
31091921
69127956
60113544
60764203
39970738
40306982
58030891
48897162
49268436
54129559
49604680
60575725
50347371
56787559
53721513
57509679
98873116
79860227
70229845
80977111
83642415
59742911
80852467
61134836
79526902
97480417
121159449
116901103
80277720
96758297
89204144
140102627
129128663
102989949
99952051
113464424
133989786
111231192
110509072
238060552
153872260
139603138
120877747
129972756
140020631
168731046
141412556
237778850
326982994
196710348
169481864
218332807
228132295
177036017
330700134
189156195
202942000
240359855
210461123
211183243
399652348
456393359
240481828
308751677
549032941
317056648
458692662
358353438
269993387
414814867
345767063
310894420
346517881
458814635
358638059
366192212
628346825
986700263
487930437
399617318
705155940
549233505
421644366
450942951
451665071
510475215
598835266
625808325
578745064
580887807
587050035
615760450
616511268
628631446
962140286
939241245
762559491
712710093
724830271
758255377
1139106661
1059708720
909574803
938873388
821261684
873309437
872587317
902608022
961418166
1030410135
1089220279
1337000441
1194505514
1202810485
1299760128
1203561303
1232271718
1341341539
1353461717
1919335785
1487389762
1437540364
1470965470
1483085648
1693849001
1694571121
1848448191
2958355232
1723869706
2173069565
1745896754
1775195339
1991828301
2164228651
3613184786
2283725793
2406371788
2397315999
3183437118
2557023020
3262585101
2573613257
2694803256
4311816949
3462793771
3994563384
3228982402
3165536591
3176934649
4860107712
3418440827
3469766460
3521092093
3715698007
3737725055
3767023640
4058921132
4447954444
4561544650
4681041792
5574250648
4803687787
5659901100
5130636277
5251826276
10463588887
8803233050
8597832767
6342471240
6394518993
6405917051
6647423229
6583977418
7185464467
6888207287
6939532920
10401392372
10340942892
18999225139
7504748695
9426924740
8506875576
9251642231
9242586442
9484729579
9934324064
10055514063
16807309423
12989894469
13334051913
13053340280
12736990233
12748388291
13579983460
12800436044
13231400647
13472184705
20170933567
16372936866
13827740207
16191175151
25675904730
16756390926
16011624271
18441199640
17749462018
22483042878
18494228673
18727316021
19419053643
22734760108
34452823911
31527752065
25485378524
25548824335
31464306254
25537426277
25979788938
35483706947
36476778039
49213768272
27299924912
29839364478
30018915358
35197590566
37221544694
42305215261
36243690691
45850988749
36190661658
37168515661
37913282316
38146369664
41462076129
42153813751
48220138632
51022804801
51034202859
51086250612
63201333632
51517215215
52837351189
74103943974
114235536491
57318840270
79375358445
59858279836
67932197674
87337755628
92516430836
72434352349
112017226290
73359177319
143550633695
74337031322
75081797977
79608445793
83615889880
115566020103
90373952383
99242943433
123951567564
136927286063
103923601801
104354566404
212009084040
186354257612
178027545775
130678017589
129753192619
127790477510
261643435655
140366550023
360886379088
220127145002
162808304732
147696208641
165455750360
153945477115
149418829299
390036629815
169982398176
228306133968
189616895816
194297554184
203166545234
232145043914
208278168205
324481711406
234107759023
257543670129
258468495099
293486322321
277449401260
268157027533
275486686151
313151959001
288062758664
297115037940
471323572767
319401227475
301641685756
467097436116
404090157199
478653231385
364279952360
442385927228
383914450000
392783441050
485727569465
411444713439
559185355885
465821838334
621042913231
890097944824
516012165228
723491384674
703315677475
641729353620
581308986534
563549444815
585177796604
944475069719
1426807062149
788004607199
871436672435
945588938894
1025007194219
908795606278
2011984858753
748194402360
2477806697087
776697891050
804228154489
1430622028320
1406110110052
1047130824868
981834003562
1079561610043
1097321151762
1101189961832
1144858431349
1345045031095
1148727241419
1525784056253
1311743847175
1675664826924
1524892293410
1992719763762
1619631074795
2149273185584
1552422556849
1580926045539
1656990008638
1730028405922
2493772272514
1758531894612
1786062158051
1851358979357
2028964828430
2599553381717
2446234992927
2242179583111
2176882761805
2697280988198
3106710101792
3516090563973
2460471088594
3255812462175
2931374921970
3470990054152
4113403347309
3077314850259
3172053631644
3209412565487
3133348602388
3282450962771
3237916054177
6103428553614
3488560300534
5375528185499
4874163750003
5618288624571
4093538562468
6240058704180
5173554505081
4623117754732
9385879516385
6169290976147
9106848925105
5391846010564
6959550354686
5537785938853
6008689772229
6210663452647
8709839570497
10882853522232
8856204678748
9412112335824
9998645940231
8864088486033
12138655641519
18249968002418
8716656317200
7582098863002
14787640521323
8967702312471
9711827187039
15136993288618
9796672259813
10160903693585
10014963765296
10929631949417
11546475711082
11400535782793
11602509463211
11748449391500
13590788635231
14718529342726
14920503023144
16291938433499
16298755180202
16438303541750
16446187349035
16549801175473
24948544214908
17293926050041
17378771122815
17597062628298
26346473435286
18679529499510
41266976458430
19957575953398
27894447896710
19811636025109
20175867458881
28194636740535
22330167732210
25339238026731
32737058721952
23350958854711
29042375441541
34035366170048
31156832884476
31212441456643
53233685923441
43640399485327
37554638581696
54240921331996
61237462113625
34672697172856
34890988678339
38637105452908
37408698653407
38491165524619
38855396958391
47706083921819
39769211978507
45515105485612
39987503483990
84390518807917
65749275322231
45681126586921
58241947533050
61779434163493
52393334296252
70981653435150
89454388989693
151233823153186
77396202137397
72227335754552
69563685851195
100416539616401
72081395826263
72299687331746
73163862697475
73382154202958
75899864178026
76264095611798
77346562483010
78624608936898
79756715462497";
    }
}
