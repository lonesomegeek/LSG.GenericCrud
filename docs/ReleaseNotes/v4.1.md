# Release Notes - v4.1

| ID | Work Item Type | Title | Assigned To | State | Tags |
|----|----------------|-----------------------------------------------------------|------------------------|----------|------|
| 33 | User Story | Repository layer is based on IEnumerable instead of IQueryable | Emmanuel Dugas-Gallant | Resolved |  |

Breaking:
- If you depend on IEnumerable<> on custom service layer, you may have to convert the data to the new IQueryable<> instead of using directly IEnumerable<>.