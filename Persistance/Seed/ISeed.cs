using Microsoft.EntityFrameworkCore;

public interface ISeed<T>
{
    T GetContext();
    void LoadSeeds();
}