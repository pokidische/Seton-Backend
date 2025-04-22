using System;
using System.Collections.Generic;
using Bogus;
using Seton_Backend;

public static class DataGenerator
{
    public static List<User> GenerateUsers(int count)
    {
        var userFaker = new Faker<User>()
            .RuleFor(u => u.Id, f => f.IndexFaker + 1)
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.PasswordHash, f => f.Internet.Password())
            .RuleFor(u => u.CreatedAt, f => f.Date.Past(2))
            .RuleFor(u => u.Notes, _ => new List<Note>());

        return userFaker.Generate(count);
    }

    public static List<Note> GenerateNotes(List<User> users, int notesPerUser)
    {
        var noteFaker = new Faker<Note>()
            .RuleFor(n => n.Id, f => f.IndexFaker + 1)
            .RuleFor(n => n.Title, f => f.Lorem.Sentence())
            .RuleFor(n => n.Content, f => f.Lorem.Paragraph())
            .RuleFor(n => n.Type, f => f.PickRandom("text", "todo", "drawing", "voice"))
            .RuleFor(n => n.IsPinned, f => f.Random.Bool())
            .RuleFor(n => n.CreatedAt, f => f.Date.Recent(30))
            .RuleFor(n => n.UpdatedAt, f => f.Date.Recent(10))
            .RuleFor(n => n.Encrypted, f => f.Random.Bool())
            .RuleFor(n => n.TodoItems, _ => new List<TodoItem>())
            .RuleFor(n => n.NoteTags, _ => new List<NoteTag>())
            .RuleFor(n => n.NoteSharings, _ => new List<NoteSharing>())
            .RuleFor(n => n.UserId, f => f.PickRandom(users).Id);

        return noteFaker.Generate(users.Count * notesPerUser);
    }

    public static List<TodoItem> GenerateTodoItems(List<Note> notes)
    {
        var todoFaker = new Faker<TodoItem>()
            .RuleFor(t => t.Id, f => f.IndexFaker + 1)
            .RuleFor(t => t.Task, f => f.Lorem.Sentence())
            .RuleFor(t => t.IsCompleted, f => f.Random.Bool())
            .RuleFor(t => t.Reminder, f => f.Date.Future(1))
            .RuleFor(t => t.NoteId, f => f.PickRandom(notes).Id);

        return todoFaker.Generate(notes.Count * 2);
    }

    public static List<Tag> GenerateTags(int count)
    {
        var tagFaker = new Faker<Tag>()
            .RuleFor(t => t.Id, f => f.IndexFaker + 1)
            .RuleFor(t => t.Name, f => f.Lorem.Word());

        return tagFaker.Generate(count);
    }

    public static List<NoteTag> GenerateNoteTags(List<Note> notes, List<Tag> tags)
    {
        var noteTagFaker = new Faker<NoteTag>()
            .RuleFor(nt => nt.NoteId, f => f.PickRandom(notes).Id)
            .RuleFor(nt => nt.TagId, f => f.PickRandom(tags).Id);

        return noteTagFaker.Generate(notes.Count);
    }

    public static List<NoteSharing> GenerateNoteSharings(List<Note> notes, List<User> users)
    {
        var noteSharingFaker = new Faker<NoteSharing>()
            .RuleFor(ns => ns.Id, f => f.IndexFaker + 1)
            .RuleFor(ns => ns.NoteId, f => f.PickRandom(notes).Id)
            .RuleFor(ns => ns.SharedWithUserId, f => f.PickRandom(users).Id)
            .RuleFor(ns => ns.CanEdit, f => f.Random.Bool());

        return noteSharingFaker.Generate(notes.Count / 2);
    }

    public static List<Folder> GenerateFolders(List<User> users, int foldersPerUser)
    {
        var folderFaker = new Faker<Folder>()
            .RuleFor(f => f.Id, f => f.IndexFaker + 1)
            .RuleFor(f => f.Name, f => f.Lorem.Word())
            .RuleFor(f => f.UserId, f => f.PickRandom(users).Id);

        return folderFaker.Generate(users.Count * foldersPerUser);
    }

    public static List<FolderNote> GenerateFolderNotes(List<Folder> folders, List<Note> notes)
    {
        var folderNoteFaker = new Faker<FolderNote>()
            .RuleFor(fn => fn.FolderId, f => f.PickRandom(folders).Id)
            .RuleFor(fn => fn.NoteId, f => f.PickRandom(notes).Id);

        return folderNoteFaker.Generate(folders.Count);
    }

    public static List<VoiceNote> GenerateVoiceNotes(List<Note> notes)
    {
        var voiceNoteFaker = new Faker<VoiceNote>()
            .RuleFor(vn => vn.Id, f => f.IndexFaker + 1)
            .RuleFor(vn => vn.AudioPath, f => f.Internet.Url())
            .RuleFor(vn => vn.Transcription, f => f.Lorem.Sentence())
            .RuleFor(vn => vn.NoteId, f => f.PickRandom(notes).Id);

        return voiceNoteFaker.Generate(notes.Count / 3);
    }

    public static List<Integration> GenerateIntegrations(List<User> users)
    {
        var integrationFaker = new Faker<Integration>()
            .RuleFor(i => i.Id, f => f.IndexFaker + 1)
            .RuleFor(i => i.Service, f => f.PickRandom("Google Drive", "Dropbox", "Notion"))
            .RuleFor(i => i.AccessToken, f => f.Random.Guid().ToString())
            .RuleFor(i => i.UserId, f => f.PickRandom(users).Id);

        return integrationFaker.Generate(users.Count / 2);
    }

    public static List<NoteVersion> GenerateNoteVersions(List<Note> notes)
    {
        var noteVersionFaker = new Faker<NoteVersion>()
            .RuleFor(nv => nv.Id, f => f.IndexFaker + 1)
            .RuleFor(nv => nv.Content, f => f.Lorem.Paragraph())
            .RuleFor(nv => nv.CreatedAt, f => f.Date.Recent(60))
            .RuleFor(nv => nv.NoteId, f => f.PickRandom(notes).Id);

        return noteVersionFaker.Generate(notes.Count / 2);
    }
}
