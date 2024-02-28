// MockWebApi

using Microsoft.AspNetCore.Mvc;
using MockWebApi.Models;
using ErrorOr;

namespace MockWebApi.Services.MockDemoItem;

public interface IMockDemoItemService {
	ErrorOr<Created>              CreateMockDemoItem(MockDemoModel mockDemoItem);
	ErrorOr<MockDemoModel>        GetMockDemoItem(Guid id);
	ErrorOr<List<MockDemoModel?>> GetMockDemoItems([FromQuery] params Guid[] ids);
	ErrorOr<Updated>              Upsert(MockDemoModel mockDemoItem);
	ErrorOr<Deleted>              DeleteMockDemoItem(Guid id);
}