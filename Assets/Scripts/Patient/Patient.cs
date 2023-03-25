public class Patient {

    private string firstName;
    private string lastName;
    private string sex;
    private string diagnosis;
    private string concern;
    private string treatment;

    public Patient(string f, string l, string s, string d, string c, string t) {
        firstName = f;
        lastName = l;
        sex = s;
        diagnosis = d;
        concern = c;
        treatment = t;
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

    public string toString() {
        return (
            "Name: " + firstName + " " + lastName + "\n" +
            "Sex: " + sex + "\n" +
            "Symptoms: " + "\n" + concern + "\n"
        );
    }

}
