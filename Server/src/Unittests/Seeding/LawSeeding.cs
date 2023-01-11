using System.Threading.Tasks;
using Context.DAL;
using NUnit.Framework;

namespace Unittests.Seeding;

public class LawSeeding : BaseUnitTests
{
    //Quelle: https://www.radlobby.at/33-stvo-novelle
    [Test]
    public async Task CreateLaw1()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Gesetzlich definierter Überholabstand";
        law.Text =
            "Beim Überholen von Radfahrenden müssen Lenkende von Kraftfahrzeugen in " +
            "Zukunft innerorts einen Mindestabstand von 1,5 Metern einhalten, außerorts " +
            "sind es 2 Meter. Damit wird eine langjährige Forderung der Radlobby erfüllt. \n\n" +
            "Jedoch mit einem Makel: Bis zu einer Geschwindigkeit des Kfz von 30 km/h darf " +
            "dieser sichere Überholabstand jedoch unterschritten werden. \n\nEs gilt die bisherige " +
            "Regelung, dass ein „der Verkehrssicherheit und der Fahrgeschwindigkeit entsprechender " +
            "seitlicher Abstand“, eingehalten werden muss.\n\n" +
            "Der gesetzlich definierte Überholabstand hebt die Verkehrssicherheit für alle und schafft " +
            "grundsätzlich klare Verhältnisse. \n\nDie 30 km/h-Ausnahme schwächt die Verbesserung " +
            "jedoch deutlich ab, zumal 30 km/h immer mehr zum üblichen Geschwindigkeitslimit innertorts wird.";
        law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);

        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw2()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Grünpfeil fürs Rad an Ampeln";
        law.Text =
            "Die Behörden haben künftig die Möglichkeit, " +
            "dem Radverkehr an einzelnen Kreuzungen das Rechtsabbiegen " +
            "bei Rot zu erlauben, indem sie dort ein Zusatzschild mit einem " +
            "grünen Pfeil und Radsymbol anbringen. \n\nAn T-Kreuzungen gibt es " +
            "eine analoge Regelung fürs Geradeausfahren. Die Radfahrenden haben " +
            "in diesen Situationen gegenüber querenden Fußgänger*innen Wartepflicht " +
            "und müssen vor dem Abbiegevorgang anhalten, ähnlich wie bei einem " +
            "Stop-Schild. \n\nDem folgt Vortasten und weiterfahren, wenn eine Gefährdung " +
            "anderer ausgeschlossen ist. Vorbild für diese Regelung ist Deutschland. " +
            "In Belgien, Dänemark, Frankreich und der Schweiz hingegen ist kein Halt nötig, " +
            "die Weiterfahrt erfolgt nach dem „Vorrang geben“-Prinzip. \n\nEine Regelung wie " +
            "in diesen Staaten hätte deutlich mehr Komfort für Radfahrende geschafft.";
law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);

        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw3()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Nebeneinander Radfahren erlaubt ";
        law.Text =
            "Die StVO erlaubt in Zukunft das Nebeneinanderfahren " +
            "am Rad unter gewissen Umständen. Wir haben uns lange für " +
            "eine generelle Ausnahme eingesetzt, die jetzt jedoch mit " +
            "zahlreichen Bedingungen kommt, was die Sache unnötig verkompliziert.\n\n" +
            "Neben Kindern bis 12 Jahren darf man zukünftig nebeneinander radeln. " +
            "Das schafft eine große Erleichterung für alle Eltern und Begleitpersonen, " +
            "die sich bisher immer die Frage stellen mussten: Vor oder nach dem Kind fahren?\n\n" +
            "Auf Straßen mit einem Tempolimit von max 30 km/h ist auch das " +
            "Nebeneinanderfahren von Erwachsenen gestattet. " +
            "Nicht jedoch auf Schienen- und Vorrangstraßen sowie gegen die " +
            "Fahrtrichtung von (geöffneten) Einbahnen. \n\nDas linke Fahrrad muss " +
            "dabei einspurig sein und es darf niemand gefährdet werden, das " +
            "Verkehrsaufkommen muss dies zulassen und andere dürfen dadurch nicht am " +
            "Überholen gehindert werden.";
            law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);

        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw4()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Annäherungen an Radfahrüberfahrten";
        law.Text =
            "Eine minimale Änderung soll es bei der Annäherungsgeschwindigkeit " +
            "an ungeregelte Radfahrerüberfahrten geben. Diesen darf man sich " +
            "weiterhin nur mit maximal 10 km/h nähern, allerdings gilt das nicht " +
            "mehr, wenn „in unmittelbarer Nähe aktuell keine Kraftfahrzeuge fahren.“\n\n " +
            "Auch diese Ausnahme verkompliziert die Sache unnötig statt wie von der " +
            "Radlobby seit vielen Jahren einfach eine \"angepasste Geschwindigkeit\" vorzuschreiben.\n\n" +
            "Um die Schutzwirkung von Radfahrüberfahrten sowie Schutzwegen zu gewährleisten, " +
            "ist eine grundsätzliche Neuregelung notwendig, die auch vom Kuratorium für " +
            "Verkehrssicherheit gefordert wird: Eine definierte Annäherungsgeschwindigkeit für " +
            "Kraftfahrzeuge vor ungeregelten Radfahrerüberfahrten, um ein sicheres Anhalten vor diesen Anlagen zu ermöglichen.";
law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw5()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Mit-Nutzung von Radwegen rechtlich möglich";
        law.Text = "Die Behörde kann zukünftig das Befahren von Radfahranlagen mit " +
                   "landwirtschaftlichen Fahrzeugen und außerorts auch mit S-Pedelecs " +
                   "(Klasse L1e) erlauben. \n\nDies ermöglicht die sogenannte \"Positivbeschilderung\", also z.B. Radweg statt Fahrverbot mit Ausnahmen.\n\n Auf Geh- und Radwegen dürfen Lenkende von Kraftfahrzeugen nur maximal 10 km/h fahren, wenn sie sich Fußgängern nähern.";
law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw6()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Durchfahrt von Kfz in Fahrradstraßen";
        law.Text =
            "Um die Einführung von Fahrradstraßen zu erleichtern, kann die Behörde „bestimmen, dass die Fahrradstraße dauernd oder zu bestimmten Zeiten oder zu Zwecken der Durchfahrt“ mit Kfz befahren werden darf.\n\n Darin lag bisher oft ein amtlicher Hinderungsgrund zur Einrichtung von Fahrradstraßen." +
            "Hier orientiert sich die StVO-Novelle an der deutschen Rechtslage.\n\n Eine Regelung, die die vermehrte Umsetzung von Fahrradstraßen erhoffen lässt. Wir weisen bei jedem Anlass darauf hin, dass dadurch Verkehrsfilter zur Kfz-Beruhigung in Fahrradstraßen umso wichtiger werden.";
       law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw7()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Reißverschlussprinzip statt Sondernachrang";
        law.Text =
            "Nach der Einführung des Reißverschlussprinzips für Radfahrstreifen analog zu normalen Fahrstreifen im Zuge der letzten Novelle: \nDies gilt zukünftig innerorts auch für parallel einmündende Radfahrende, die einen Radweg verlassen.";
        law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw8()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Eindämmung überzogener Strafen";
        law.Text =
            "Das Mehrfachstrafen durch Aufsummieren einzelner fehlender Reflektoren wird behoben. \n\nZukünftig ist sind auch mehrere Ausstattungsmängel aus dem § 1 Abs. 1 der Fahrradverordnung als eine Verwaltungsübertretung gewertet.";

        law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw9()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Hineinragen in Radwege";
        law.Text =
            "Das Verparken von Radinfrastruktur ist insbesondere in Städten ein großes Problem. Bisher ist  das Überragen von Bodenmarkierungen nach § 9 Abs. 7 untersagt, ein ähnliches Verbot gilt zukünftig für das Hineinragen von Kfz in Radwege und Gehwege (bei Gehwegen ausg. geringfügiges Überragen durch Seitenspiegel oder Stoßstange).\n\n" +
            "Für Ladetätigkeiten für bis zu 10 Minuten dürfen Gehsteige bis auf 1,5 m verengt werden. Bei Einbauten & Gegenständen sind zukünftig dauerhaft 1,5 m frei zu halten, mit temporären Ausnahmen bei Bauarbeiten o.ä.";

        law.Source = "https://www.ris.bka.gv.at/Dokumente/BgblAuth/BGBLA_2022_I_122/BGBLA_2022_I_122.html";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw10()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "StVO VI. Abschnitt (Besondere Vorschriften für den Verkehr mit Fahrrädern und Motorfahrrädern)";
        law.Title = "§ 65. Benützung von Fahrrädern";
        law.Text = "(1) Der Lenker eines Fahrrades (Radfahrer) muß mindestens zwölf Jahre alt sein; wer ein Fahrrad schiebt, gilt nicht als Radfahrer. Kinder unter zwölf Jahren dürfen ein Fahrrad nur unter Aufsicht einer Person, die das 16. Lebensjahr vollendet hat, oder mit behördlicher Bewilligung lenken.\n"+
        "(2) Die Behörde hat auf Antrag des gesetzlichen Vertreters des Kindes die Bewilligung nach Abs. 1 zu erteilen, wenn das Kind\n"+
        "1. das 9. Lebensjahr vollendet hat und die 4. Schulstufe besucht oder\n"+
        "2. das 10. Lebensjahr vollendet hat"+
        "und anzunehmen ist, dass es die erforderliche körperliche und geistige Eignung sowie Kenntnisse der straßenpolizeilichen Vorschriften besitzt. Die Bewilligung gilt für das ganze Bundesgebiet, sofern nicht der gesetzliche Vertreter des Kindes eine örtlich eingeschränkte Geltung beantragt hat. Sie ist unter Bedingungen und mit Auflagen zu erteilen, wenn dies die Verkehrssicherheit erfordert. Die Behörde kann die Bewilligung widerrufen, wenn sich die Verkehrsverhältnisse seit der Erteilung geändert haben oder nachträglich zutage tritt, daß das Kind die erforderliche körperliche oder geistige Eignung nicht besitzt. Über die von ihr erteilte Bewilligung hat die Behörde eine Bestätigung, den Radfahrausweis, auszustellen. Inhalt und Form des Radfahrausweises hat der Bundesminister für Verkehr, Innovation und Technologie durch Verordnung zu bestimmen. Der auf Grund dieser Bestimmung gestellte Antrag, die erteilte Bewilligung und der ausgestellte Radfahrausweis sind von Bundesstempelgebühren befreit.\n"+
        "(3) Radfahrer, die auf dem Fahrrad Personen mitführen, müssen das 16. Lebensjahr vollendet haben. Ist die mitgeführte Person noch nicht acht Jahre alt, so muß für sie ein eigener, der Größe des Kindes entsprechender Sitz vorhanden sein. Ist die mitgeführte Person mehr als acht Jahre alt, so darf nur ein Fahrrad verwendet werden, das hinsichtlich seiner Bauart den Anforderungen der Produktsicherheitsbestimmungen für Fahrräder zum Transport mehrerer Personen (§ 104 Abs. 8) entspricht.";
        law.Source = "https://www.ris.bka.gv.at/GeltendeFassung.wxe?Abfrage=Bundesnormen&Gesetzesnummer=10011336";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw11()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "StVO VI. Abschnitt (Besondere Vorschriften für den Verkehr mit Fahrrädern und Motorfahrrädern)";
        law.Title = "§ 66. Beschaffenheit von Fahrrädern, Fahrradanhängern und Kindersitzen";
        law.Text = "(1) Fahrräder müssen der Größe des Benützers entsprechen. Fahrräder, Fahrradanhänger und Kindersitze müssen in einem Zustand erhalten werden, der den Anforderungen der Produktsicherheitsbestimmungen für Fahrräder (§ 104 Abs. 8) entspricht.\n"+
        "(2) Der Bundesminister für Verkehr, Innovation und Technologie hat unter Bedachtnahme auf die Verkehrssicherheit und den Stand der Technik durch Verordnung festzulegen:\n"+
        "1. unter welchen Voraussetzungen bestimmte Teile der Ausrüstung von Fahrrädern oder Fahrradanhängern entfallen können;\n"+
        "2. unter welchen Voraussetzungen die Beförderung von Kindern in Kindersitzen oder Personen mit Fahrradanhängern und mehrspurigen Fahrrädern zulässig ist;\n"+
        "3. das Ladegewicht, das bei der Beförderung von Lasten oder Personen mit Fahrrädern oder mit Fahrradanhängern nicht überschritten werden darf.";
        law.Source = "https://www.ris.bka.gv.at/GeltendeFassung.wxe?Abfrage=Bundesnormen&Gesetzesnummer=10011336";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw12()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "StVO VI. Abschnitt (Besondere Vorschriften für den Verkehr mit Fahrrädern und Motorfahrrädern)";
        law.Title = "§ 67. Fahrradstraße";
        law.Text = "(1) Die Behörde kann, wenn es der Sicherheit, Leichtigkeit oder Flüssigkeit des Verkehrs, insbesondere des Fahrradverkehrs, oder der Entflechtung des Verkehrs dient oder aufgrund der Lage, Widmung oder Beschaffenheit eines Gebäudes oder Gebietes im öffentlichen Interesse gelegen ist, durch Verordnung Straßen oder Straßenabschnitte dauernd oder zeitweilig zu Fahrradstraßen erklären. In einer solchen Fahrradstraße ist außer dem Fahrradverkehr jeder Fahrzeugverkehr verboten; ausgenommen davon ist das Befahren mit den in § 76a Abs. 5 genannten Fahrzeugen sowie das Befahren zum Zweck des Zu- und Abfahrens.\n"+
        "(2) Die Behörde kann in der Verordnung nach Abs. 1 nach Maßgabe der Erfordernisse und unter Bedachtnahme auf die örtlichen Gegebenheiten bestimmen, dass die Fahrradstraße auch mit anderen als den in Abs. 1 genannten Fahrzeugen dauernd oder zu bestimmten Zeiten oder zu Zwecken der Durchfahrt befahren werden darf; das Queren von Fahrradstraßen ist jedenfalls erlaubt.\n"+
        "(3) Die Lenker von Fahrzeugen dürfen in Fahrradstraßen nicht schneller als 30 km/h fahren. Radfahrer dürfen weder gefährdet noch behindert werden."+
        "(4) Für die Kundmachung einer Verordnung nach Abs. 1 gelten die Bestimmungen des § 44 Abs. 1 mit der Maßgabe, dass am Anfang und am Ende einer Fahrradstraße die betreffenden Hinweiszeichen (§ 53 Abs. 1 Z 26 und 29) anzubringen sind.";
        law.Source = "https://www.ris.bka.gv.at/GeltendeFassung.wxe?Abfrage=Bundesnormen&Gesetzesnummer=10011336";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw13()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "StVO VI. Abschnitt (Besondere Vorschriften für den Verkehr mit Fahrrädern und Motorfahrrädern)";
        law.Title = "§ 68. Fahrradverkehr";
        law.Text = "(1) Auf Straßen mit einer Radfahranlage ist mit einspurigen Fahrrädern ohne Anhänger die Radfahranlage zu benützen, wenn der Abstand der Naben des Vorderrades und des Hinterrades nicht mehr als 1,7 m beträgt und das Befahren der Radfahranlage in der vom Radfahrer beabsichtigten Fahrtrichtung gemäß § 8a erlaubt ist. Mit Fahrrädern mit einem Anhänger, der nicht breiter als 100 cm ist, mit mehrspurigen Fahrrädern, die nicht breiter als 100 cm sind, sowie bei Trainingsfahrten mit Rennfahrrädern darf die Radfahranlage benützt werden; mit Fahrrädern mit einem sonstigen Anhänger oder mit sonstigen mehrspurigen Fahrrädern ist die für den übrigen Verkehr bestimmte Fahrbahn zu benützen. Auf Gehsteigen und Gehwegen ist das Radfahren in Längsrichtung verboten. Auf Geh- und Radwegen haben sich Radfahrer so zu verhalten, dass Fußgänger nicht gefährdet werden.\n"+
        "(1a) Wenn es der Leichtigkeit und Flüssigkeit des Fahrradverkehrs dient und aus Gründen der Leichtigkeit und Flüssigkeit des übrigen Verkehrs sowie der Verkehrssicherheit keine Bedenken dagegen bestehen, kann die Behörde bestimmen, dass abweichend von Abs. 1 von Radfahrern mit einspurigen Fahrrädern ohne Anhänger ein Radweg oder ein Geh- und Radweg benützt werden darf, aber nicht muss. Derartige Radwege oder Geh- und Radwege sind mit den Zeichen gemäß § 53 Abs. 1 Z 27 bis 29 anzuzeigen.\n"+
        "(2) Radfahrer dürfen auf Radwegen, in Fahrradstraßen, in Wohnstraßen und in Begegnungszonen neben einem anderen Radfahrer fahren sowie bei Trainingsfahrten mit Rennfahrrädern nebeneinander fahren. In Fußgängerzonen dürfen Radfahrer neben einem anderen Radfahrer fahren, wenn das Befahren der Fußgängerzone mit Fahrrädern erlaubt ist. Auf allen sonstigen Radfahranlagen und auf Fahrbahnen, auf denen eine Höchstgeschwindigkeit von höchstens 30 km/h und Fahrradverkehr erlaubt sind, ausgenommen auf Schienenstraßen, Vorrangstraßen und Einbahnstraßen gegen die Fahrtrichtung, darf mit einem einspurigen Fahrrad neben einem anderen Radfahrer gefahren werden, sofern niemand gefährdet wird, das Verkehrsaufkommen es zulässt und andere Verkehrsteilnehmer nicht am Überholen gehindert werden. Bei der Begleitung eines Kindes unter 12 Jahren durch eine Person, die mindestens 16 Jahre alt ist, ist das Fahren neben dem Kind, ausgenommen auf Schienenstraßen, zulässig. Beim Fahren neben einem anderen Radfahrer darf nur der äußerst rechte Fahrstreifen benützt werden und Fahrzeuge des Kraftfahrlinienverkehrs dürfen nicht behindert werden. Radfahrer in Gruppen ab zehn Personen ist das Queren einer Kreuzung im Verband durch den übrigen Fahrzeugverkehr zu erlauben. Dabei sind beim Einfahren in die Kreuzung die für Radfahrer geltenden Vorrangregeln zu beachten; der voran fahrende Radfahrer hat im Kreuzungsbereich den übrigen Fahrzeuglenkern das Ende der Gruppe durch Handzeichen zu signalisieren und erforderlichenfalls vom Fahrrad abzusteigen. Der erste und letzte Radfahrer der Gruppe haben dabei eine reflektierende Warnweste zu tragen.\n"+
        "(3) Es ist verboten,\n"+
        "a) auf einem Fahrrad freihändig zu fahren oder die Füße während der Fahrt von den Treteinrichtungen zu entfernen,\n"+
        "b) sich mit einem Fahrrad an ein anderes Fahrzeug anzuhängen, um sich ziehen zu lassen,\n"+
        "c) Fahrräder in einer nicht verkehrsgemäßen Art zu gebrauchen, zum Beispiel Karussellfahren, Wettfahren und dgl.,\n"+
        "d) beim Radfahren andere Fahrzeuge oder Kleinfahrzeuge mitzuführen,\n"+
        "e) während des Radfahrens ohne Benützung einer Freisprecheinrichtung zu telefonieren; hinsichtlich der Anforderungen für Freisprecheinrichtungen gilt § 102 Abs. 3 KFG 1967.\n"+
        "(3a) Radfahrer dürfen sich Radfahrerüberfahrten, wo der Verkehr nicht durch Arm- oder Lichtzeichen geregelt wird, nur mit einer Geschwindigkeit von höchstens 10 km/h nähern und diese nicht unmittelbar vor einem herannahenden Fahrzeug und für dessen Lenker überraschend befahren, es sei denn, dass in unmittelbarer Nähe keine Kraftfahrzeuge aktuell fahren.\n"+
        "(4) Fahrräder sind so aufzustellen, daß sie nicht umfallen oder den Verkehr behindern können. Ist ein Gehsteig mehr als 2,5 m breit, so dürfen Fahrräder auch auf dem Gehsteig abgestellt werden; dies gilt nicht im Haltestellenbereich öffentlicher Verkehrsmittel, außer wenn dort Fahrradständer aufgestellt sind. Auf einem Gehsteig sind Fahrräder platzsparend so aufzustellen, daß Fußgänger nicht behindert und Sachen nicht beschädigt werden.\n"+
        "(5) Gegenstände, die am Anzeigen der Fahrtrichtungsänderung hindern oder die freie Sicht oder die Bewegungsfreiheit des Radfahrers beeinträchtigen oder Personen gefährden oder Sachen beschädigen können, wie zum Beispiel ungeschützte Sägen oder Sensen, geöffnete Schirme und dgl., dürfen am Fahrrad nicht mitgeführt werden.\n"+
        "(6) Kinder unter 12 Jahren müssen beim Rad fahren, beim Transport in einem Fahrradanhänger und wenn sie auf einem Fahrrad mitgeführt werden, einen Sturzhelm in bestimmungsgemäßer Weise gebrauchen. Dies gilt nicht, wenn der Gebrauch des Helms wegen der körperlichen Beschaffenheit des Kindes nicht möglich ist. Wer ein Kind beim Rad fahren beaufsichtigt, auf einem Fahrrad mitführt oder in einem Fahrradanhänger transportiert, muss dafür sorgen, dass das Kind den Sturzhelm in bestimmungsgemäßer Weise gebraucht. Im Falle eines Verkehrsunfalls begründet das Nichttragen des Helms kein Mitverschulden im Sinne des § 1304 des allgemeinen bürgerlichen Gesetzbuches, JGS Nr. 946/1811, an den Folgen des Unfalls.";
        law.Source = "https://www.ris.bka.gv.at/GeltendeFassung.wxe?Abfrage=Bundesnormen&Gesetzesnummer=10011336";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw14()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "StVO VI. Abschnitt (Besondere Vorschriften für den Verkehr mit Fahrrädern und Motorfahrrädern)";
        law.Title = "§ 69. Motorfahrräder";
        law.Text = "(1) Mit Motorfahrrädern ist ausschließlich die Fahrbahn zu benützen.\n"+
        "(2) Für die Lenker von Motorfahrrädern gelten die Bestimmungen des § 68 Abs. 3 bis 5 über das Verhalten von Radfahrern sinngemäß.\n"+
        "Überdies ist ihnen verboten:\n"+
        "a) Das Nebeneinanderfahren mit anderen Motorfahrrädern oder Fahrrädern,\n"+
        "b) Motorfahrräder neben einem anderen Motorfahrrad oder Fahrrad zu schieben,\n"+
        "(Anm.: lit. c aufgehoben durch BGBl. I Nr. 122/2022)";
        law.Source = "https://www.ris.bka.gv.at/GeltendeFassung.wxe?Abfrage=Bundesnormen&Gesetzesnummer=10011336";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }
}