const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5190/api/v1';

const fetchJson = async (url, options = {}) => {
  const response = await fetch(url, options);

  if (!response.ok) {
    const text = await response.text();
    throw new Error(text || `Request failed: ${response.status}`);
  }

  if (response.status === 204) {
    return null;
  }

  const text = await response.text();

  if (!text) {
    return null;
  }

  try {
    return JSON.parse(text);
  } catch {
    return text;
  }
};

export const productApi = {
  getAll: async () => fetchJson(`${API_BASE_URL}/products`),
  getById: async (id) => fetchJson(`${API_BASE_URL}/products/${id}`),
  create: async (product) => fetchJson(`${API_BASE_URL}/products`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(product),
  }),
  update: async (id, product) => fetchJson(`${API_BASE_URL}/products/${id}`, {
    method: 'PATCH',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(product),
  }),
  delete: async (id) => fetchJson(`${API_BASE_URL}/products/${id}`, { method: 'DELETE' }),
};

export const brandApi = {
  getAll: async () => fetchJson(`${API_BASE_URL}/brands`),
  getById: async (id) => fetchJson(`${API_BASE_URL}/brands/${id}`),
  create: async (brand) => fetchJson(`${API_BASE_URL}/brands`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(brand),
  }),
  update: async (id, brand) => fetchJson(`${API_BASE_URL}/brands/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(brand),
  }),
  delete: async (id) => fetchJson(`${API_BASE_URL}/brands/${id}`, { method: 'DELETE' }),
};

export const userApi = {
  getAll: async () => fetchJson(`${API_BASE_URL}/users`),
  getById: async (id) => fetchJson(`${API_BASE_URL}/users/${id}`),
  create: async (user) => fetchJson(`${API_BASE_URL}/users`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(user),
  }),
  update: async (id, user) => fetchJson(`${API_BASE_URL}/users/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(user),
  }),
  topUp: async (id, amount) => fetchJson(`${API_BASE_URL}/users/${id}`, {
    method: 'PATCH',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ money: amount }),
  }),
  delete: async (id) => fetchJson(`${API_BASE_URL}/users/${id}`, { method: 'DELETE' }),
};

export const reviewApi = {
  create: async (userId, productId, review) => fetchJson(`${API_BASE_URL}/users/${userId}/add-review/${productId}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(review),
  }),
};

export const cartApi = {
  addItem: async (userId, productId, quantity) => fetchJson(`${API_BASE_URL}/users/${userId}/cart/add-item`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ productId, amount: quantity }),
  }),
  buy: async (userId) => fetchJson(`${API_BASE_URL}/users/${userId}/cart/buy`, {
    method: 'POST',
  }),
};

export const purchaseApi = {
  getByUser: async (userId) => fetchJson(`${API_BASE_URL}/user/${userId}/purchases`),
  getLatest: async (count = 6) => fetchJson(`${API_BASE_URL}/purchases/latest?count=${count}`),
};
