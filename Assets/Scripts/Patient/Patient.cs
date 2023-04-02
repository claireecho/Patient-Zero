public class Patient {

    private string firstName;
    private string lastName;
    private string sex;
    private string diagnosis;
    private string concern;
    private string treatment;
    private string treated;

    public Patient(string f, string l, string s, string d, string c, string t) {
        firstName = f;
        lastName = l;
        sex = s;
        diagnosis = d;
        concern = c;
        treatment = t;
        treated = "";
    }

    public string getFirstName() {
        return firstName;
    }

    public string getLastName() {
        return lastName;
    }

    public string getSex() {
        return sex;
    }

    public string getConcern() {
        return concern;
    }

    public string getDiagnosis() {
        return diagnosis;
    }

    public string getTreatment() {
        return treatment;
    }

    public string getTreated() {
        return treated;
    }

    public void setFirstName(string f) {
        firstName = f;
    }

    public void setLastName(string l) {
        lastName = l;
    }

    public void setSetx(string s) {
        sex = s;
    }

    public void setConcern(string c) {
        concern = c;
    }

    public void setDiagnosis(string d) {
        diagnosis = d;
    }

    public void setTreatment(string t) {
        treatment = t;
    }

    public void setTreated(string t) {
        treated = t;
    }

    public bool Success() {
        treated = "YES";
        return true;
    }

    public bool Failure() {
        treated = "NO";
        return false;
    }

    public string toString() {
        string temp = 
            "Name: " + firstName + " " + lastName + "\n" +
            "Sex: " + sex + "\n" +
            "Symptoms: " + "\n";
        foreach (string j in concern.Split("\n")) {
            if (j != "") {
                temp += "  - " + j;
            }
            if (j != concern.Split("\n")[concern.Split("\n").Length - 1]) {
                temp += "\n";
            }
        }
        return temp;
    }

}
