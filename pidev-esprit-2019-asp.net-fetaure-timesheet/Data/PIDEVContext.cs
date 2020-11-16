namespace Data
{
    using System;

    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Data;
    using System.Data.Entity;

    public partial class PIDEVContext : DbContext
    {
        public PIDEVContext()
            : base("name=PIDEVContext")
        {
        }

        public virtual DbSet<centreformation> centreformations { get; set; }
        public virtual DbSet<commentaire> commentaires { get; set; }
        public virtual DbSet<dayoff> dayoffs { get; set; }
        public virtual DbSet<employee_training> employee_training { get; set; }
        public virtual DbSet<eval360> eval360 { get; set; }
        public virtual DbSet<evaluation> evaluations { get; set; }
        public virtual DbSet<feedback> feedbacks { get; set; }
        public virtual DbSet<mission> missions { get; set; }
        public virtual DbSet<missionemployeeaffectation> missionemployeeaffectations { get; set; }
        public virtual DbSet<month> months { get; set; }
        public virtual DbSet<notification> notifications { get; set; }
        public virtual DbSet<objective> objectives { get; set; }
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<post_skills> post_skills { get; set; }
        public virtual DbSet<Project> projects { get; set; }
        public virtual DbSet<publication> publications { get; set; }
        public virtual DbSet<skill> skills { get; set; }
        public virtual DbSet<Team> teams { get; set; }
        public virtual DbSet<Ticket> tickets { get; set; }
        public virtual DbSet<training> trainings { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<centreformation>()
                .Property(e => e.adresse)
                .IsUnicode(false);

            modelBuilder.Entity<centreformation>()
                .Property(e => e.libelle)
                .IsUnicode(false);

            modelBuilder.Entity<centreformation>()
                .HasMany(e => e.trainings)
                .WithMany(e => e.centreformations)
                .Map(m => m.ToTable("training_centreformation", "pidev").MapLeftKey("centreFormations_id").MapRightKey("Trainings_id"));

            modelBuilder.Entity<commentaire>()
                .Property(e => e.contenu)
                .IsUnicode(false);

            modelBuilder.Entity<dayoff>()
                .Property(e => e.typeDayOFF)
                .IsUnicode(false);

            modelBuilder.Entity<employee_training>()
                .Property(e => e.test)
                .IsUnicode(false);

            modelBuilder.Entity<eval360>()
                .Property(e => e.evalDetails)
                .IsUnicode(false);

            modelBuilder.Entity<eval360>()
                .HasMany(e => e.feedbacks)
                .WithRequired(e => e.eval360)
                .HasForeignKey(e => e.idEval360)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<evaluation>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<evaluation>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<feedback>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<mission>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<mission>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<mission>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<mission>()
                .Property(e => e.Objective)
                .IsUnicode(false);

            modelBuilder.Entity<mission>()
                .HasMany(e => e.missionemployeeaffectations)
                .WithRequired(e => e.mission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<missionemployeeaffectation>()
                .Property(e => e.note)
                .IsUnicode(false);

            modelBuilder.Entity<month>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<month>()
                .HasMany(e => e.dayoffs)
                .WithOptional(e => e.month)
                .HasForeignKey(e => e.month_id);

            modelBuilder.Entity<notification>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.forUserHavingRole)
                .IsUnicode(false);

            modelBuilder.Entity<notification>()
                .Property(e => e.notifType)
                .IsUnicode(false);

            modelBuilder.Entity<objective>()
                .Property(e => e.category)
                .IsUnicode(false);

            modelBuilder.Entity<objective>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<objective>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<objective>()
                .HasMany(e => e.evaluations)
                .WithRequired(e => e.objective)
                .HasForeignKey(e => e.idObjective)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<post>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<post>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<post>()
                .HasMany(e => e.post_skills)
                .WithRequired(e => e.post)
                .HasForeignKey(e => e.idPost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.tickets)
                .WithOptional(e => e.project)
                .HasForeignKey(e => e.project_id);

            modelBuilder.Entity<publication>()
                .Property(e => e.commentPub)
                .IsUnicode(false);

            modelBuilder.Entity<publication>()
                .Property(e => e.subjectPub)
                .IsUnicode(false);

            modelBuilder.Entity<publication>()
                .HasMany(e => e.commentaires)
                .WithOptional(e => e.publication)
                .HasForeignKey(e => e.publication_idPub);

            modelBuilder.Entity<skill>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<skill>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<skill>()
                .HasMany(e => e.post_skills)
                .WithRequired(e => e.skill)
                .HasForeignKey(e => e.idSkill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.departementEnum)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .Property(e => e.teamName)
                .IsUnicode(false);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.projects)
                .WithOptional(e => e.team)
                .HasForeignKey(e => e.team_id);

            modelBuilder.Entity<Team>()
                .HasMany(e => e.users)
                .WithOptional(e => e.team)
                .HasForeignKey(e => e.team_id);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Ticket>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<training>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<training>()
                .Property(e => e.specification)
                .IsUnicode(false);

            modelBuilder.Entity<training>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<training>()
                .HasMany(e => e.employee_training)
                .WithRequired(e => e.training)
                .HasForeignKey(e => e.idTraining)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .Property(e => e.user_role)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.firstName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.lastName)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.cvDetails)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.gitLink)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.eval360)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.concernedEmployee_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.evaluations)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.feedbacks)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idGivenByEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.missionemployeeaffectations)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.idEmployee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.publications)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.employee_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.teams)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.manager_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.tickets)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.employee_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.user1)
                .WithOptional(e => e.user2)
                .HasForeignKey(e => e.humanRessource_id);
        }
    }
}
