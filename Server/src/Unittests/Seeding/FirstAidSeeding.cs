using System.Threading.Tasks;
using Context.DAL;
using NUnit.Framework;

namespace Unittests.Seeding;

public class FirstAidSeeding : BaseUnitTests
{
    [Test]
    public async Task CreateFirstAid1()
    {
        Info aid = new Info();

        aid.Category = Category.FirstAid;
        aid.Title = "112 anrufen";
        aid.Action = Action.EmergencyCall;
        aid.Icon = "phone-portrait-outline";

        Info returnval = await MongoUoW.Infos.InsertOneAsync(aid);

        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateFirstAid2()
    {
        Info aid = new Info();

        aid.Category = Category.FirstAid;
        aid.Title = "Stabile Seitenlage";
        aid.Text = "Eine reglose Person wird zunächst auf dem Rücken gelagert, um Bewusstsein und Atmung zu überprüfen (Diagnostischer Block), weswegen dies meist die Ausgangsposition für weitere Maßnahmen ist.\n\n"
            + "Wenn der Betroffene trotz gestörten Bewusstseins selbständig atmet, wird er in die stabile Seitenlage verbracht. Zum Schutz gegen Witterungseinflüsse wird er danach vorzugsweise in eine Rettungsdecke eingewickelt, um Auskühlung beziehungsweise Überhitzung zu vermeiden.\n\n"
            + "Bis zum Eintreffen des per Notruf alarmierten Rettungsdienstes wird der Betroffene ständig überwacht. So können bei einer Verschlechterung des Zustandes rechtzeitig weitere Maßnahmen eingeleitet und erwachende Betroffene beruhigt werden. \n\n"
            + "Die stabile Seitenlage ist auch im Rettungsdienst essenziell, da sie die einfachste Methode der Atemwegssicherung darstellt.\n\n"
            + "Weist der Betroffene eine Verletzung im Bereich des Brustkorbs oder der Lunge auf, wird er auf die verletzte Seite gedreht, damit die dann oben liegende, unbeeinträchtigte Lungenhälfte sich während der Einatmung frei entfalten kann und eventuelle Blutungen abgedrückt werden.\n\n"
            + "Schwangere Frauen werden tendenziell auf der linken Seite gelagert, da so der gemeinsame Kreislauf von Mutter und Fötus am besten entlastet werden kann.";
        aid.Icon = "bag-add";

        Info returnval = await MongoUoW.Infos.InsertOneAsync(aid);

        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateFirstAid3()
    {
        Info aid = new Info();

        aid.Category = Category.FirstAid;
        aid.Title = "Blutende Wunde";
        aid.Text = "Eine reglose Person wird zunächst auf dem Rücken gelagert, um Bewusstsein und Atmung zu überprüfen (Diagnostischer Block), weswegen dies meist die Ausgangsposition für weitere Maßnahmen ist.\n\n"
            + "Wenn der Betroffene trotz gestörten Bewusstseins selbständig atmet, wird er in die stabile Seitenlage verbracht. Zum Schutz gegen Witterungseinflüsse wird er danach vorzugsweise in eine Rettungsdecke eingewickelt, um Auskühlung beziehungsweise Überhitzung zu vermeiden.\n\n"
            + "Bis zum Eintreffen des per Notruf alarmierten Rettungsdienstes wird der Betroffene ständig überwacht. So können bei einer Verschlechterung des Zustandes rechtzeitig weitere Maßnahmen eingeleitet und erwachende Betroffene beruhigt werden. \n\n"
            + "Die stabile Seitenlage ist auch im Rettungsdienst essenziell, da sie die einfachste Methode der Atemwegssicherung darstellt.\n\n"
            + "Weist der Betroffene eine Verletzung im Bereich des Brustkorbs oder der Lunge auf, wird er auf die verletzte Seite gedreht, damit die dann oben liegende, unbeeinträchtigte Lungenhälfte sich während der Einatmung frei entfalten kann und eventuelle Blutungen abgedrückt werden.\n\n"
            + "Schwangere Frauen werden tendenziell auf der linken Seite gelagert, da so der gemeinsame Kreislauf von Mutter und Fötus am besten entlastet werden kann.";
        aid.Icon = "egg";

        Info returnval = await MongoUoW.Infos.InsertOneAsync(aid);

        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateFirstAid4()
    {
        Info aid = new Info();

        aid.Category = Category.FirstAid;
        aid.Title = "Brandverletzung";
        aid.Text = "Eine reglose Person wird zunächst auf dem Rücken gelagert, um Bewusstsein und Atmung zu überprüfen (Diagnostischer Block), weswegen dies meist die Ausgangsposition für weitere Maßnahmen ist.\n\n"
            + "Wenn der Betroffene trotz gestörten Bewusstseins selbständig atmet, wird er in die stabile Seitenlage verbracht. Zum Schutz gegen Witterungseinflüsse wird er danach vorzugsweise in eine Rettungsdecke eingewickelt, um Auskühlung beziehungsweise Überhitzung zu vermeiden.\n\n"
            + "Bis zum Eintreffen des per Notruf alarmierten Rettungsdienstes wird der Betroffene ständig überwacht. So können bei einer Verschlechterung des Zustandes rechtzeitig weitere Maßnahmen eingeleitet und erwachende Betroffene beruhigt werden. \n\n"
            + "Die stabile Seitenlage ist auch im Rettungsdienst essenziell, da sie die einfachste Methode der Atemwegssicherung darstellt.\n\n"
            + "Weist der Betroffene eine Verletzung im Bereich des Brustkorbs oder der Lunge auf, wird er auf die verletzte Seite gedreht, damit die dann oben liegende, unbeeinträchtigte Lungenhälfte sich während der Einatmung frei entfalten kann und eventuelle Blutungen abgedrückt werden.\n\n"
            + "Schwangere Frauen werden tendenziell auf der linken Seite gelagert, da so der gemeinsame Kreislauf von Mutter und Fötus am besten entlastet werden kann.";
        aid.Icon = "flame";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(aid);

        Assert.NotNull(returnval);
    }

    [Test]
    public async Task CreateFirstAid5()
    {
        Info aid = new Info();

        aid.Category = Category.FirstAid;
        aid.Title = "Unfallstelle absichern";
        aid.Text = "Eine reglose Person wird zunächst auf dem Rücken gelagert, um Bewusstsein und Atmung zu überprüfen (Diagnostischer Block), weswegen dies meist die Ausgangsposition für weitere Maßnahmen ist.\n\n"
            + "Wenn der Betroffene trotz gestörten Bewusstseins selbständig atmet, wird er in die stabile Seitenlage verbracht. Zum Schutz gegen Witterungseinflüsse wird er danach vorzugsweise in eine Rettungsdecke eingewickelt, um Auskühlung beziehungsweise Überhitzung zu vermeiden.\n\n"
            + "Bis zum Eintreffen des per Notruf alarmierten Rettungsdienstes wird der Betroffene ständig überwacht. So können bei einer Verschlechterung des Zustandes rechtzeitig weitere Maßnahmen eingeleitet und erwachende Betroffene beruhigt werden. \n\n"
            + "Die stabile Seitenlage ist auch im Rettungsdienst essenziell, da sie die einfachste Methode der Atemwegssicherung darstellt.\n\n"
            + "Weist der Betroffene eine Verletzung im Bereich des Brustkorbs oder der Lunge auf, wird er auf die verletzte Seite gedreht, damit die dann oben liegende, unbeeinträchtigte Lungenhälfte sich während der Einatmung frei entfalten kann und eventuelle Blutungen abgedrückt werden.\n\n"
            + "Schwangere Frauen werden tendenziell auf der linken Seite gelagert, da so der gemeinsame Kreislauf von Mutter und Fötus am besten entlastet werden kann.";
        aid.Icon = "lock-closed";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(aid);

        Assert.NotNull(returnval);
    }


    [Test]
    public async Task CreateFirstAid6()
    {
        Info aid = new Info();

        aid.Category = Category.FirstAid;
        aid.Title = "Unfallbericht";
        aid.Text = "Das musst du beim Unfallbericht wissen\n\n\n\n"
            + "<ul><li>Personendaten</li>\n\n"
            + "<li>Fahrzeugdaten von allen Beteiligten</li>\n\n"
            + "<li>Versicherungsnummer</li>\n\n"
            + "<li>Verletzte</li>\n\n"
            + "<li>Fahrzeugschaden</li>\n\n"
            + "<li>Zeugen</li></ul>";
        aid.Action = Action.AccidentReport;
        aid.Icon = "person";
        Info returnval = await MongoUoW.Infos.InsertOneAsync(aid);

        Assert.NotNull(returnval);
    }
}