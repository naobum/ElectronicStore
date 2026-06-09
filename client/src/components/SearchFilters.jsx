import '../styles/SearchFilters.css';

export default function SearchFilters({
  search,
  onSearch,
  brandId,
  onBrandChange,
  minRating,
  onMinRatingChange,
  brands,
}) {
  return (
    <section className="filters-panel">
      <div className="filter-row">
        <label>
          Поиск:
          <input type="search" value={search} onChange={(e) => onSearch(e.target.value)} placeholder="Название товара" />
        </label>
        <label>
          Бренд:
          <select value={brandId} onChange={(e) => onBrandChange(e.target.value)}>
            <option value="">Все бренды</option>
            {brands.map((brand) => (
              <option key={brand.id} value={brand.id}>{brand.name}</option>
            ))}
          </select>
        </label>
        <label>
          Мин. рейтинг:
          <select value={minRating} onChange={(e) => onMinRatingChange(e.target.value)}>
            <option value="">Любой</option>
            <option value="1">1+</option>
            <option value="2">2+</option>
            <option value="3">3+</option>
            <option value="4">4+</option>
            <option value="5">5</option>
          </select>
        </label>
      </div>
    </section>
  );
}
