import java.time.LocalDate;
import java.util.*;
import java.util.stream.Collectors;

public class Main {
    public static void main(String[] args) {
        RandomList<String> myList = new RandomList<>();
        myList.add("Apple");
        myList.add("Banana");
        myList.add("Cherry");

        System.out.println("Is empty: " + myList.isEmpty());
        System.out.println("Random get (max 2): " + myList.get(2));

        List<GoldPrice> prices = Arrays.asList(
            new GoldPrice(LocalDate.parse("2025-03-03"), 364.46),
            new GoldPrice(LocalDate.parse("2025-03-04"), 371.52),
            new GoldPrice(LocalDate.parse("2025-03-05"), 369.45),
            new GoldPrice(LocalDate.parse("2025-03-06"), 363.26)
        );

        List<GoldPrice> top3 = prices.stream()
            .sorted(Comparator.comparingDouble(GoldPrice::getPrice).reversed())
            .limit(3)
            .collect(Collectors.toList());

        System.out.println("Top 3 Prices:");
        top3.forEach(System.out::println);
    }
}
