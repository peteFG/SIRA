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
            "sind es 2 Meter. Damit wird eine langjährige Forderung der Radlobby erfüllt. " +
            "Jedoch mit einem Makel: Bis zu einer Geschwindigkeit des Kfz von 30 km/h darf " +
            "dieser sichere Überholabstand jedoch unterschritten werden. Es gilt die bisherige " +
            "Regelung, dass ein „der Verkehrssicherheit und der Fahrgeschwindigkeit entsprechender " +
            "seitlicher Abstand“, eingehalten werden muss.\n" +
            "Der gesetzlich definierte Überholabstand hebt die Verkehrssicherheit für alle und schafft " +
            "grundsätzlich klare Verhältnisse. Die 30 km/h-Ausnahme schwächt die Verbesserung " +
            "jedoch deutlich ab, zumal 30 km/h immer mehr zum üblichen Geschwindigkeitslimit innertorts wird.";

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
            "grünen Pfeil und Radsymbol anbringen. An T-Kreuzungen gibt es " +
            "eine analoge Regelung fürs Geradeausfahren. Die Radfahrenden haben " +
            "in diesen Situationen gegenüber querenden Fußgänger*innen Wartepflicht " +
            "und müssen vor dem Abbiegevorgang anhalten, ähnlich wie bei einem " +
            "Stop-Schild. Dem folgt Vortasten und weiterfahren, wenn eine Gefährdung " +
            "anderer ausgeschlossen ist. Vorbild für diese Regelung ist Deutschland. " +
            "In Belgien, Dänemark, Frankreich und der Schweiz hingegen ist kein Halt nötig, " +
            "die Weiterfahrt erfolgt nach dem „Vorrang geben“-Prinzip. Eine Regelung wie " +
            "in diesen Staaten hätte deutlich mehr Komfort für Radfahrende geschafft.";

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
            "zahlreichen Bedingungen kommt, was die Sache unnötig verkompliziert." +
            "Neben Kindern bis 12 Jahren darf man zukünftig nebeneinander radeln. " +
            "Das schafft eine große Erleichterung für alle Eltern und Begleitpersonen, " +
            "die sich bisher immer die Frage stellen mussten: Vor oder nach dem Kind fahren?" +
            "Auf Straßen mit einem Tempolimit von max 30 km/h ist auch das " +
            "Nebeneinanderfahren von Erwachsenen gestattet. " +
            "Nicht jedoch auf Schienen- und Vorrangstraßen sowie gegen die " +
            "Fahrtrichtung von (geöffneten) Einbahnen. Das linke Fahrrad muss " +
            "dabei einspurig sein und es darf niemand gefährdet werden, das " +
            "Verkehrsaufkommen muss dies zulassen und andere dürfen dadurch nicht am " +
            "Überholen gehindert werden.";
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
            "mehr, wenn „in unmittelbarer Nähe aktuell keine Kraftfahrzeuge fahren.“ " +
            "Auch diese Ausnahme verkompliziert die Sache unnötig statt wie von der " +
            "Radlobby seit vielen Jahren einfach eine \"angepasste Geschwindigkeit\" vorzuschreiben." +
            "Um die Schutzwirkung von Radfahrüberfahrten sowie Schutzwegen zu gewährleisten, " +
            "ist eine grundsätzliche Neuregelung notwendig, die auch vom Kuratorium für " +
            "Verkehrssicherheit gefordert wird: Eine definierte Annäherungsgeschwindigkeit für " +
            "Kraftfahrzeuge vor ungeregelten Radfahrerüberfahrten, um ein sicheres Anhalten vor diesen Anlagen zu ermöglichen.";

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
                   "(Klasse L1e) erlauben. Dies ermöglicht die sogenannte \"Positivbeschilderung\", also z.B. Radweg statt Fahrverbot mit Ausnahmen. Auf Geh- und Radwegen dürfen Lenkende von Kraftfahrzeugen nur maximal 10 km/h fahren, wenn sie sich Fußgängern nähern.";

        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw6()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Durchfahrt von Kfz in Fahrradstraßen kann ermöglicht werden";
        law.Text =
            "Um die Einführung von Fahrradstraßen zu erleichtern, kann die Behörde „bestimmen, dass die Fahrradstraße dauernd oder zu bestimmten Zeiten oder zu Zwecken der Durchfahrt“ mit Kfz befahren werden darf. Darin lag bisher oft ein amtlicher Hinderungsgrund zur Einrichtung von Fahrradstraßen." +
            "Hier orientiert sich die StVO-Novelle an der deutschen Rechtslage. Eine Regelung, die die vermehrte Umsetzung von Fahrradstraßen erhoffen lässt. Wir weisen bei jedem Anlass darauf hin, dass dadurch Verkehrsfilter zur Kfz-Beruhigung in Fahrradstraßen umso wichtiger werden.";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw7()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Reißverschlussprinzip statt Sondernachrang bei parallel einmündenden Radwegen";
        law.Text =
            "Nach der Einführung des Reißverschlussprinzips für Radfahrstreifen analog zu normalen Fahrstreifen im Zuge der letzten Novelle: Dies gilt zukünftig innerorts auch für parallel einmündende Radfahrende, die einen Radweg verlassen.";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw8()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Eindämmung überzogener Strafen bei Ausstattungsmängeln";
        law.Text =
            "Das Mehrfachstrafen durch Aufsummieren einzelner fehlender Reflektoren wird behoben. Zukünftig ist sind auch mehrere Ausstattungsmängel aus dem § 1 Abs. 1 der Fahrradverordnung als eine Verwaltungsübertretung gewertet.";

        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateLaw9()
    {
        Info law = new Info();

        law.Category = Category.Law;
        law.Section = "33. StVO-Novelle";
        law.Title = "Das Hineinragen in Radwege wird verboten, in Gehwege deutlich eingeschränkt";
        law.Text =
            "Das Verparken von Radinfrastruktur ist insbesondere in Städten ein großes Problem. Bisher ist  das Überragen von Bodenmarkierungen nach § 9 Abs. 7 untersagt, ein ähnliches Verbot gilt zukünftig für das Hineinragen von Kfz in Radwege und Gehwege (bei Gehwegen ausg. geringfügiges Überragen durch Seitenspiegel oder Stoßstange)." +
            "Für Ladetätigkeiten für bis zu 10 Minuten dürfen Gehsteige bis auf 1,5 m verengt werden. Bei Einbauten & Gegenständen sind zukünftig dauerhaft 1,5 m frei zu halten, mit temporären Ausnahmen bei Bauarbeiten o.ä.";

        Info returnval = await MongoUoW.Infos.InsertOneAsync(law);
        Assert.NotNull(returnval);
    }
}