import java.time.LocalDate;

public class GoldPrice {
    private LocalDate date;
    private double price;

    public GoldPrice(LocalDate date, double price) {
        this.date = date;
        this.price = price;
    }

    public LocalDate getDate() {
        return date;
    }

    public double getPrice() {
        return price;
    }

    @Override
    public String toString() {
        return date + " - " + price;
    }
}
