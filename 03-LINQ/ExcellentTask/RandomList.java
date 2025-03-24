import java.util.*;

public class RandomList<T> {
    private List<T> items = new ArrayList<>();
    private Random random = new Random();

    public void add(T element) {
        if (random.nextBoolean()) {
            items.add(0, element);
            System.out.println("Added at beginning: " + element);
        } else {
            items.add(element);
            System.out.println("Added at end: " + element);
        }
    }

    public T get(int index) {
        if (items.isEmpty()) {
            throw new IllegalStateException("List is empty.");
        }
        int randIndex = random.nextInt(Math.min(index + 1, items.size()));
        return items.get(randIndex);
    }

    public boolean isEmpty() {
        return items.isEmpty();
    }
}
