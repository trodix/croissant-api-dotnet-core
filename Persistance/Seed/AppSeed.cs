using CroissantApi.Persistence.Context;
using CroissantApi.Models;

namespace CroissantApi.Persistence.Seed
{
    public class AppSeed : ISeed<CroissantContext>
    {
        private readonly CroissantContext _context;
        public AppSeed(CroissantContext contect)
        {
            _context = contect;
        }

        public CroissantContext GetContext()
        {
            return _context;
        }

        public void LoadSeeds()
        {
            MakeSeeds();
            _context.SaveChanges();
        }

        private void MakeSeeds()
        {
            Rule r1 = _context.Rules.Add(new Rule() { 
                Id = 1,
                Name = "Ton ordi !", 
                Description = "Fais un Win+L sinon tu paie les croissants, tu as 1 chance !!!",
                CoinsCapacity = 1
            }).Entity;

            Rule r2 = _context.Rules.Add(new Rule() { 
                Id = 2,
                Name = "La chaise !", 
                Description = "Quand tu as fini la journée, tu met ta chaise sur ta table, sinon tu paie les croissants. Tu as 3 chances.",
                CoinsCapacity = 3
            }).Entity;

            Rule r3 = _context.Rules.Add(new Rule() { 
                Id = 3,
                Name = "La porte !", 
                Description = "Quand tu sors tu ferme la porte derière toi, sinon tu paie les croissants. Tu as 3 chances.",
                CoinsCapacity = 3
            }).Entity;

            Team t1 = _context.Teams.Add(new Team() { 
                Id = 1,
                Name = "CESI RIL B2 aka Croissanistan"
            }).Entity;

            TeamRule tr1 = _context.TeamRules.Add(new TeamRule() {
                Team = t1,
                Rule = r1
            }).Entity;

            TeamRule tr2 = _context.TeamRules.Add(new TeamRule() {
                Team = t1,
                Rule = r2
            }).Entity;

            TeamRule tr3 = _context.TeamRules.Add(new TeamRule() {
                Team = t1,
                Rule = r3
            }).Entity;

            User u1 = _context.Users.Add(new User() { 
                Id = 1,
                Lastname = "Vallet", 
                Firstname = "Sébastien",
                BirthDate = new System.DateTime(1997, 09, 18),
                Team = t1
            }).Entity;

            User u2 = _context.Users.Add(new User() { 
                Id = 2,
                Lastname = "Bayon", 
                Firstname = "Sylvain",
                BirthDate = new System.DateTime(1970, 01, 01),
                Team = t1
            }).Entity;

            UserRule ur1 = _context.UserRules.Add(new UserRule() {
                User = u1,
                Rule = r1,
                CoinsQuantity = 1
            }).Entity;

            UserRule ur2 = _context.UserRules.Add(new UserRule() {
                User = u2,
                Rule = r3,
                CoinsQuantity = 2
            }).Entity;
        }
    }
}